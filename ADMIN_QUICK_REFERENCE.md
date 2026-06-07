# Admin Control Center - Quick Reference Guide

## Access the Admin Panel
1. Navigate to `AdminPanel.aspx`
2. You must be logged in as an admin user
3. Page displays "Admin Control Center" with your username

---

## Member Management Quick Start

### Adding a New Member
```
1. Scroll to "Club Member Management" section
2. Fill in:
   - Full Name (required) ✓
   - Position (required) ✓
   - Department
   - Email
   - Phone
   - Photo URL
   - Bio
3. Ensure "Active" checkbox is checked (for visibility)
4. Click "Save Member" button
5. ✓ Member added to list below
```

### Editing a Member
```
1. Find member in table below form
2. Click "Edit" button in their row
3. Form auto-fills with their data
4. Make changes
5. Click "Save Member"
6. ✓ Changes applied
```

### Deleting a Member
```
1. Find member in table below form
2. Click "Delete" button in their row
3. Confirm when prompted
4. ✓ Member removed
```

---

## Event Management Quick Start

### Adding a New Event
```
1. Scroll to "Event Management" section
2. Fill in:
   - Slug (required) - URL-friendly name ✓
   - Title (required) - Event name ✓
   - Event Date (required) - Pick from calendar ✓
   - Venue - Location
   - Format (required) - e.g., "In-person", "Online" ✓
   - Fee - Cost/Free
   - Tagline - Brief description
   - Summary - Full description (multi-line)
   - Eligibility - Who can attend
   - Payment Note - Payment instructions
   - Guidelines - Rules (enter one per line)
   - Background Image URL - Hero image
3. Check "Upcoming Event" to show on homepage
4. Check "Active" to make visible on site
5. Click "Save Event"
6. ✓ Event added to list below
```

### Editing an Event
```
1. Find event in table below form
2. Click "Edit" button in their row
3. Form auto-fills with event data
4. Guidelines appear in multi-line text box
5. Make changes
6. Click "Save Event"
7. ✓ Changes applied
```

### Deleting an Event
```
1. Find event in table below form
2. Click "Delete" button in their row
3. Confirm when prompted
4. ✓ Event removed
```

---

## Dashboard Sidebar Information

The left sidebar shows:

**Quick Stats**
- Club Members: [count] - total members in database
- All Events: [count] - total events created
- Upcoming Events: [count] - events flagged as upcoming and active

**Quick Access Links**
- Members - Jump to member section
- Events - Jump to event section
- View Site - Exit admin and view public site

---

## Important Notes

### Member Fields
| Field | Required | Format |
|-------|----------|--------|
| Full Name | ✓ Yes | Text |
| Position | ✓ Yes | Text |
| Department | No | Text |
| Email | No | Email |
| Phone | No | Phone |
| Photo URL | No | URL |
| Bio | No | Multi-line text |
| Active | No | Checkbox |

### Event Fields
| Field | Required | Format |
|-------|----------|--------|
| Slug | ✓ Yes | Text (no spaces) |
| Title | ✓ Yes | Text |
| Event Date | ✓ Yes | Date picker |
| Venue | No | Text |
| Format | ✓ Yes | Text |
| Fee | No | Text |
| Tagline | No | Text |
| Summary | No | Multi-line |
| Eligibility | No | Text |
| Payment Note | No | Text |
| Guidelines | No | Multi-line (one per line) |
| Background URL | No | URL |
| Upcoming | No | Checkbox |
| Active | No | Checkbox |

---

## Tips & Best Practices

✓ **Always fill required fields** - form won't save without them
✓ **Use Clear button** - to reset form if you make mistakes
✓ **Check Active status** - unchecked means hidden from public
✓ **Use Upcoming checkbox** - to feature events on homepage
✓ **URLs should be complete** - include http:// or https://
✓ **Dates are auto-formatted** - saved in readable format
✓ **Guidelines help users** - separate multiple guidelines with line breaks

---

## Troubleshooting

| Problem | Solution |
|---------|----------|
| Form won't save | Check that all required fields (marked ✓) are filled |
| Member/Event not appearing | Check "Active" checkbox is marked |
| Wrong member showing in edit | Make sure you clicked correct row's "Edit" button |
| Can't delete | Click "Delete" on the correct row, then confirm |
| Can't access Admin Panel | Log in as admin user first, check session |

---

## Column Reference

### Member Table Columns
- Full Name - Person's name
- Position - Role/Title
- Department - Department name
- Email - Contact email
- Active - Visibility on public site

### Event Table Columns
- Title - Event name
- Slug - URL identifier
- Date - Event date (formatted)
- Format - Event type/format
- Upcoming - Featured on homepage
- Active - Visible on site

---

**Last Updated**: 2026-06-07
**Version**: 1.0
