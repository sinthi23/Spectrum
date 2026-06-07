# Admin Control Center - Architecture & Implementation Details

## System Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     AdminPanel.aspx                         │
│              (Presentation Layer - WebForms)               │
├─────────────────────────────────────────────────────────────┤
│ UI Components:                                              │
│ • Member Form (TextBoxes, CheckBoxes)                       │
│ • Member GridView (Edit/Delete buttons)                     │
│ • Event Form (TextBoxes, CheckBoxes)                        │
│ • Event GridView (Edit/Delete buttons)                      │
│ • Dashboard Stats (Member/Event counts)                     │
│ • Session Sidebar (Admin info, Quick links)                 │
└────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                AdminPanel.aspx.cs                           │
│         (Business Logic Layer - Page Code-Behind)          │
├─────────────────────────────────────────────────────────────┤
│ Methods:                                                    │
│ • Page_Load() - Initialize dashboard                       │
│ • MemberSaveButton_Click() - Save/Update members           │
│ • MemberClearButton_Click() - Clear form                   │
│ • MembersGridView_SelectedIndexChanged() - Load for edit    │
│ • MembersGridView_RowDeleting() - Delete member            │
│ • EventSaveButton_Click() - Save/Update events             │
│ • EventClearButton_Click() - Clear form                    │
│ • EventsGridView_SelectedIndexChanged() - Load for edit     │
│ • EventsGridView_RowDeleting() - Delete event              │
│ • BindDashboard() - Refresh stats & tables                 │
└────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│              Data Access Layer (Repository)                 │
├─────────────────────────────────────────────────────────────┤
│ ClubMemberRepository:                                       │
│   • GetAll() → List<ClubMember>                             │
│   • GetById(id) → ClubMember                                │
│   • Insert(member) → void                                   │
│   • Update(member) → void                                   │
│   • Delete(id) → void                                       │
│                                                              │
│ EventRepository:                                             │
│   • GetAll() → List<EventInfo>                              │
│   • GetById(id) → EventInfo                                 │
│   • Insert(event) → void                                    │
│   • Update(event) → void                                    │
│   • Delete(id) → void                                       │
│   • GetUpcoming() → List<EventInfo>                         │
│   • GetBySlug(slug) → EventInfo                             │
└────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                    DbGateway                                │
│            (Database Connection Management)                 │
└────────────────────────────────────────────────────────────┘
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                  SQL Server Database                        │
├─────────────────────────────────────────────────────────────┤
│ Tables:                                                     │
│ • ClubMembers (MemberId, FullName, Position, ...)          │
│ • ClubEvents (EventId, Slug, Title, ...)                   │
└────────────────────────────────────────────────────────────┘
```

---

## Database Schema

### ClubMembers Table
```sql
CREATE TABLE ClubMembers (
    MemberId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(255) NOT NULL,
    Position NVARCHAR(255) NOT NULL,
    Department NVARCHAR(255),
    Email NVARCHAR(255),
    Phone NVARCHAR(20),
    Bio NVARCHAR(MAX),
    PhotoUrl NVARCHAR(500),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
);
```

### ClubEvents Table
```sql
CREATE TABLE ClubEvents (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    Slug NVARCHAR(255) NOT NULL UNIQUE,
    Title NVARCHAR(255) NOT NULL,
    EventDate DATETIME NOT NULL,
    Venue NVARCHAR(255),
    Format NVARCHAR(100) NOT NULL,
    Fee NVARCHAR(100),
    Tagline NVARCHAR(500),
    Summary NVARCHAR(MAX),
    Eligibility NVARCHAR(MAX),
    PaymentNote NVARCHAR(MAX),
    Guidelines NVARCHAR(MAX),  -- JSON array stored as string
    BackgroundUrl NVARCHAR(500),
    IsUpcoming BIT DEFAULT 1,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
);
```

---

## Data Models (Classes)

### ClubMember.cs
```csharp
public sealed class ClubMember
{
    public int MemberId { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Bio { get; set; }
    public string PhotoUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

### EventInfo.cs
```csharp
public sealed class EventInfo
{
    public int EventId { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }              // Display format
    public DateTime EventDate { get; set; }       // Actual date for calculations
    public string Venue { get; set; }
    public string Format { get; set; }
    public string Fee { get; set; }
    public string Tagline { get; set; }
    public string Summary { get; set; }
    public string Eligibility { get; set; }
    public string PaymentNote { get; set; }
    public string[] Guidelines { get; set; }
    public string BackgroundUrl { get; set; }
    public bool IsUpcoming { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

---

## Page Flow Diagrams

### Member Management Flow
```
┌─────────────┐
│  User Page  │
│   Load      │
└──────┬──────┘
       ↓
┌─────────────────────┐
│ Page_Load()         │
│ • BindDashboard()   │
│ • ClearMemberForm() │
└──────┬──────────────┘
       ↓
    ┌──────────────────┐
    │ Display Member   │
    │ Table & Form     │
    └────┬──────┬──────┘
         │      └────────────────┐
         ↓                        ↓
    ┌────────────────┐    ┌───────────────────┐
    │ User Edits &   │    │ User Clicks       │
    │ Clicks "Save"  │    │ "Edit" Button     │
    └────┬───────────┘    └───────┬───────────┘
         ↓                        ↓
    ┌────────────────┐    ┌───────────────────┐
    │Validate        │    │MembersGridView_   │
    │Required Fields │    │SelectedIndex      │
    │• FullName      │    │ • Load member     │
    │• Position      │    │ • Populate form   │
    └────┬───────────┘    └───────────────────┘
         ↓
    ┌─────────────────────────────────────┐
    │ ClubMemberRepository.Insert/Update()│
    │ • Save to database                  │
    │ • Return success/error              │
    └────┬────────────────────────────────┘
         ↓
    ┌──────────────────────┐
    │ ShowMessage()        │
    │ ClearMemberForm()    │
    │ BindDashboard()      │
    └──────────────────────┘
```

### Event Management Flow
```
┌─────────────┐
│  User Page  │
│   Load      │
└──────┬──────┘
       ↓
┌──────────────────┐
│ Page_Load()      │
│ • BindDashboard()│
│ • ClearEventForm │
└──────┬───────────┘
       ↓
    ┌──────────────────┐
    │ Display Event    │
    │ Table & Form     │
    └────┬──────┬──────┘
         │      └────────────────┐
         ↓                        ↓
    ┌────────────────┐    ┌───────────────────┐
    │ User Edits &   │    │ User Clicks       │
    │ Clicks "Save"  │    │ "Edit" Button     │
    └────┬───────────┘    └───────┬───────────┘
         ↓                        ↓
    ┌──────────────────┐    ┌───────────────────┐
    │Parse Event Date  │    │EventsGridView_    │
    │(yyyy-MM-dd)      │    │SelectedIndex      │
    │Validate Required:│    │ • Load event      │
    │• Slug            │    │ • Convert date    │
    │• Title           │    │ • Populate form   │
    │• Format          │    │ • Split guidelines│
    └────┬─────────────┘    └───────────────────┘
         ↓
    ┌─────────────────────────────────────┐
    │ EventRepository.Insert/Update()     │
    │ • Save to database                  │
    │ • Join guidelines with semicolons   │
    │ • Return success/error              │
    └────┬────────────────────────────────┘
         ↓
    ┌──────────────────────┐
    │ ShowMessage()        │
    │ ClearEventForm()     │
    │ BindDashboard()      │
    └──────────────────────┘
```

### Delete Flow (Member or Event)
```
┌─────────────────────┐
│ User clicks "Delete"│
│ on table row        │
└──────┬──────────────┘
       ↓
┌──────────────────────┐
│ Browser Confirmation│
│ Dialog Shows        │
└──────┬──────────────┘
       ↓
    ┌──────────────────┐
    │ User Confirms    │
    └────┬─────────────┘
         ↓
    ┌──────────────────────────────────────┐
    │ MembersGridView_RowDeleting() or     │
    │ EventsGridView_RowDeleting()         │
    │ • Extract ID from DataKeys           │
    │ • Call Repository.Delete(id)         │
    └────┬─────────────────────────────────┘
         ↓
    ┌─────────────────────────────────────┐
    │ Repository.Delete()                 │
    │ • Execute DELETE SQL command        │
    │ • Remove from database              │
    └────┬────────────────────────────────┘
         ↓
    ┌──────────────────────┐
    │ ShowMessage()        │
    │ ClearForm()          │
    │ BindDashboard()      │
    └──────────────────────┘
```

---

## Form Validation Rules

### Member Form Validation
```
┌─────────────────────────────────────┐
│ MemberSaveButton_Click()            │
├─────────────────────────────────────┤
│ 1. Trim all text inputs             │
│ 2. Validate required fields:        │
│    ✓ FullName != null/empty         │
│    ✓ Position != null/empty         │
│ 3. If validation fails:             │
│    • Show error message             │
│    • Return without saving          │
│ 4. If memberId > 0:                 │
│    • Call ClubMemberRepository.     │
│      Update(member)                 │
│ 5. Else:                            │
│    • Call ClubMemberRepository.     │
│      Insert(member)                 │
│ 6. Show success message             │
│ 7. Clear form                       │
│ 8. Refresh dashboard                │
└─────────────────────────────────────┘
```

### Event Form Validation
```
┌─────────────────────────────────────┐
│ EventSaveButton_Click()             │
├─────────────────────────────────────┤
│ 1. Try to parse event date:         │
│    • Format: yyyy-MM-dd (HTML5)     │
│    • If fails: show error & return  │
│ 2. Validate required fields:        │
│    ✓ Slug != null/empty             │
│    ✓ Title != null/empty            │
│    ✓ Format != null/empty           │
│ 3. If validation fails:             │
│    • Show error message             │
│    • Return without saving          │
│ 4. Split guidelines by newlines     │
│ 5. If eventId > 0:                  │
│    • Call EventRepository.Update()  │
│ 6. Else:                            │
│    • Call EventRepository.Insert()  │
│ 7. Show success message             │
│ 8. Clear form                       │
│ 9. Refresh dashboard                │
└─────────────────────────────────────┘
```

---

## Security Considerations

1. **Authentication**: AdminPageBase enforces admin login before page loads
2. **Authorization**: Session tokens validated on each request
3. **SQL Injection Prevention**: All queries use parameterized SQL with SqlCommand.Parameters
4. **CSRF Protection**: ASP.NET ViewState provides CSRF token
5. **Data Validation**: Server-side validation of all inputs before database save
6. **Session Management**: Session ID regenerated on login, cleared on logout

---

## Performance Optimizations

1. **GridView Binding**: Only binds on initial load (IsPostBack check)
2. **Database Queries**: Parameterized queries for efficiency
3. **Connection Pooling**: SqlConnection automatically pooled via DbGateway
4. **Data Caching**: EventRepository caches seed data to avoid repeated queries
5. **Lazy Loading**: Forms and tables only rendered when needed

---

## Error Handling

1. **Form Validation Errors**: Caught before database operations, user-friendly messages displayed
2. **Database Errors**: Handled gracefully with try-catch in repositories
3. **Session Errors**: User redirected to login if session invalid
4. **Data Binding Errors**: GridView handles empty result sets gracefully

---

**Last Updated**: 2026-06-07
