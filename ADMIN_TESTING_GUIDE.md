# Admin Control Center - Testing Guide

## Complete Testing Checklist

### Prerequisites
- [ ] Project builds successfully with `dotnet build`
- [ ] Admin user account exists in database
- [ ] You can log in as admin
- [ ] AdminPanel.aspx is accessible after login

---

## Member Management Testing

### Test: Add a New Member
**Steps:**
1. Login to admin panel
2. Navigate to "Club Member Management" section
3. Fill in the form:
   - Full Name: "John Smith"
   - Position: "Vice President"
   - Department: "Computer Science"
   - Email: "john.smith@kuet.ac.bd"
   - Phone: "+88-01234-567890"
   - Photo URL: "https://example.com/photo.jpg"
   - Bio: "Experienced leader with 5 years in the club"
   - Active: ✓ (checked)
4. Click "Save Member"

**Expected Results:**
- ✓ Success message appears: "Member added successfully."
- ✓ Form clears (all fields empty except Active checked)
- ✓ Member count in sidebar increases by 1
- ✓ New member appears in table below form
- ✓ New member visible in database query

**Pass/Fail:** ___________

---

### Test: Add Member Without Required Fields
**Steps:**
1. Leave "Full Name" empty
2. Fill "Position": "Treasurer"
3. Click "Save Member"

**Expected Results:**
- ✓ Error message: "Member full name and position are required."
- ✓ Form not cleared (data preserved)
- ✓ Member count unchanged
- ✓ No new member added to database

**Pass/Fail:** ___________

### Test: Edit Existing Member
**Steps:**
1. In member table, click "Edit" on any member row
2. Change:
   - Full Name: append " (Updated)"
   - Department: change to different value
3. Click "Save Member"

**Expected Results:**
- ✓ Success message: "Member updated successfully."
- ✓ Form clears
- ✓ Member count unchanged (no new member)
- ✓ Table reflects updated values
- ✓ Database shows updated member

**Pass/Fail:** ___________

### Test: Delete Member
**Steps:**
1. In member table, click "Delete" on any member row
2. Confirm when browser dialog appears

**Expected Results:**
- ✓ Member removed from table immediately
- ✓ Member count in sidebar decreases by 1
- ✓ Success message: "Member deleted successfully."
- ✓ Form clears
- ✓ Member no longer in database

**Pass/Fail:** ___________

### Test: Cancel Member Deletion
**Steps:**
1. In member table, click "Delete" on any member row
2. Click "Cancel" (or close) in browser dialog

**Expected Results:**
- ✓ Member remains in table
- ✓ Member count unchanged
- ✓ Database unchanged
- ✓ Page returns to normal state

**Pass/Fail:** ___________

### Test: Clear Member Form
**Steps:**
1. Fill member form with test data
2. Click "Clear" button

**Expected Results:**
- ✓ All fields clear except Active (checked)
- ✓ MemberId hidden field resets to "0"
- ✓ Form ready for new member

**Pass/Fail:** ___________

### Test: Member Table Display
**Steps:**
1. View member table after adding 2-3 members
2. Verify all columns display correctly

**Expected Results:**
- ✓ Columns show: Full Name, Position, Department, Email, Active
- ✓ All member data displays correctly
- ✓ Edit and Delete buttons visible and clickable
- ✓ Table scrolls if many members

**Pass/Fail:** ___________

---

## Event Management Testing

### Test: Add a New Event
**Steps:**
1. Navigate to "Event Management" section
2. Fill in the form:
   - Slug: "tech-summit-2026"
   - Title: "Tech Summit 2026"
   - Event Date: (pick a future date, e.g., 2026-12-15)
   - Venue: "KUET Main Hall"
   - Format: "In-person"
   - Fee: "500 BDT"
   - Tagline: "Largest tech conference in the region"
   - Summary: "A comprehensive summit about emerging technologies..."
   - Eligibility: "All KUET students and faculty"
   - Payment Note: "Payment via bKash or Cash"
   - Guidelines: "Line 1: No recording\nLine 2: Professional attire required"
   - Background URL: "https://example.com/banner.jpg"
   - Upcoming: ✓ (checked)
   - Active: ✓ (checked)
3. Click "Save Event"

**Expected Results:**
- ✓ Success message: "Event added successfully."
- ✓ Form clears (all fields empty except checkboxes checked)
- ✓ Event count in sidebar increases by 1
- ✓ Upcoming count increases by 1
- ✓ New event appears in table below form
- ✓ Event visible in database query

**Pass/Fail:** ___________

### Test: Add Event Without Required Fields
**Steps:**
1. Leave "Slug" empty
2. Fill Title: "Test Event"
3. Fill Format: "Online"
4. Pick a date
5. Click "Save Event"

**Expected Results:**
- ✓ Error message: "Event slug, title, and format are required."
- ✓ Form not cleared (data preserved)
- ✓ Event counts unchanged
- ✓ No new event added to database

**Pass/Fail:** ___________

### Test: Invalid Event Date
**Steps:**
1. Fill in event form normally
2. Enter invalid date in "Event Date" field (e.g., "abc" or "13-50-2026")
3. Click "Save Event"

**Expected Results:**
- ✓ Error message: "A valid event date is required."
- ✓ Form not cleared
- ✓ Event counts unchanged
- ✓ No event added to database

**Pass/Fail:** ___________

### Test: Edit Existing Event
**Steps:**
1. In event table, click "Edit" on any event row
2. Verify form populated with event data
3. Verify guidelines appear in multi-line text box (one per line)
4. Change:
   - Title: append " - Updated"
   - Venue: change location
   - Fee: change amount
5. Click "Save Event"

**Expected Results:**
- ✓ Success message: "Event updated successfully."
- ✓ Form clears
- ✓ Event count unchanged
- ✓ Table reflects updated values
- ✓ Database shows updated event

**Pass/Fail:** ___________

### Test: Edit Event - Change Upcoming Status
**Steps:**
1. Click "Edit" on an upcoming event
2. Uncheck "Upcoming Event" checkbox
3. Click "Save Event"

**Expected Results:**
- ✓ Event updated successfully
- ✓ Upcoming count in sidebar decreases by 1
- ✓ IsUpcoming value changes in table/database

**Pass/Fail:** ___________

### Test: Edit Event - Change Active Status
**Steps:**
1. Click "Edit" on an active event
2. Uncheck "Active" checkbox
3. Click "Save Event"

**Expected Results:**
- ✓ Event updated successfully
- ✓ Event may no longer appear on public site (if visibility controlled by Active flag)
- ✓ IsActive value changes in table/database

**Pass/Fail:** ___________

### Test: Delete Event
**Steps:**
1. In event table, click "Delete" on any event row
2. Confirm when browser dialog appears

**Expected Results:**
- ✓ Event removed from table immediately
- ✓ Event count in sidebar decreases by 1
- ✓ Upcoming count updates if was marked upcoming
- ✓ Success message: "Event deleted successfully."
- ✓ Form clears
- ✓ Event no longer in database

**Pass/Fail:** ___________

### Test: Clear Event Form
**Steps:**
1. Fill event form with test data
2. Click "Clear" button

**Expected Results:**
- ✓ All fields clear
- ✓ Checkboxes reset (Upcoming and Active checked)
- ✓ EventId hidden field resets to "0"
- ✓ Form ready for new event

**Pass/Fail:** ___________

### Test: Event Table Display
**Steps:**
1. View event table after adding 2-3 events
2. Verify all columns display correctly

**Expected Results:**
- ✓ Columns show: Title, Slug, Date, Format, Upcoming, Active
- ✓ All event data displays correctly
- ✓ Edit and Delete buttons visible and clickable
- ✓ Dates display in readable format
- ✓ Table scrolls if many events

**Pass/Fail:** ___________

### Test: Guidelines Handling
**Steps:**
1. Add event with guidelines:
   ```
   Guidelines for participants
   No photos without permission
   Mandatory attendance sheet
   ```
2. Save event
3. Click Edit to load it again
4. Check guidelines field

**Expected Results:**
- ✓ Guidelines saved to database
- ✓ Guidelines split by newlines on load
- ✓ Multi-line textbox shows each guideline on separate line
- ✓ Can edit individual guidelines

**Pass/Fail:** ___________

---

## Dashboard & Sidebar Testing

### Test: Dashboard Statistics Update
**Steps:**
1. Note current stats in sidebar
2. Add a new member
3. Check member count increased by 1

**Expected Results:**
- ✓ Member count increases immediately
- ✓ Stat refreshes on each operation

**Pass/Fail:** ___________

### Test: Dashboard Statistics - Events
**Steps:**
1. Note current stats
2. Add new event with "Upcoming" checked
3. Check counts updated correctly

**Expected Results:**
- ✓ All Events count increases by 1
- ✓ Upcoming Events count increases by 1 (if upcoming)
- ✓ Stats update immediately

**Pass/Fail:** ___________

### Test: Admin Name Display
**Steps:**
1. Login as admin
2. Check sidebar for "Signed in as [Name]"

**Expected Results:**
- ✓ Your admin name displays correctly
- ✓ Shows FullName if available, else UserName

**Pass/Fail:** ___________

### Test: Quick Access Links
**Steps:**
1. Click "Members" link in Quick Access
2. Check page scrolls to members section

**Expected Results:**
- ✓ Page scrolls to #membersSection

**Pass/Fail:** ___________

---

## Session & Security Testing

### Test: Session Persistence
**Steps:**
1. Add a member
2. Refresh the page (F5)
3. Check you're still logged in

**Expected Results:**
- ✓ Session persists
- ✓ Still authenticated
- ✓ Member data saved

**Pass/Fail:** ___________

### Test: Logout
**Steps:**
1. Click "Logout" in navigation
2. Try to access AdminPanel.aspx directly

**Expected Results:**
- ✓ Session clears
- ✓ Redirected to login page
- ✓ Cannot access admin without re-login

**Pass/Fail:** ___________

### Test: Session Timeout
**Steps:**
1. Wait for session to expire (if configured, typically 20 minutes)
2. Try to perform admin action

**Expected Results:**
- ✓ Session expires
- ✓ User redirected to login
- ✓ Cannot perform operations

**Pass/Fail:** ___________

---

## Data Persistence Testing

### Test: Data Survives Page Refresh
**Steps:**
1. Add a member
2. See it in table
3. Refresh page (F5)
4. Check member still in table

**Expected Results:**
- ✓ Member persists in database
- ✓ Data survives page refresh
- ✓ Can modify same member across sessions

**Pass/Fail:** ___________

### Test: Data Visible in Database
**Steps:**
1. Add member/event through admin panel
2. Query database directly: `SELECT * FROM ClubMembers`
3. Verify record exists

**Expected Results:**
- ✓ Data saved to database
- ✓ All fields populated correctly
- ✓ Timestamps recorded

**Pass/Fail:** ___________

---

## Browser Compatibility Testing

Test these browsers if available:
- [ ] Chrome (latest)
- [ ] Firefox (latest)
- [ ] Edge (latest)
- [ ] Safari (latest)

**Expected Results for Each:**
- ✓ Form renders correctly
- ✓ All buttons functional
- ✓ Tables display properly
- ✓ Date picker works (HTML5)
- ✓ Checkboxes work
- ✓ Text areas work

---

## Responsive Design Testing

### Test: Mobile View
**Steps:**
1. Open admin panel on mobile device or use Chrome DevTools (F12)
2. Set viewport to mobile size (375px width)
3. Test adding a member/event

**Expected Results:**
- ✓ Form readable on mobile
- ✓ Buttons clickable with touch
- ✓ Text inputs usable
- ✓ Table scrollable horizontally if needed
- ✓ No layout breaks

**Pass/Fail:** ___________

---

## Performance Testing

### Test: Large Data Set
**Steps:**
1. Add 50+ members to database
2. Load admin panel
3. Check page load time
4. Scroll through table

**Expected Results:**
- ✓ Page loads in under 3 seconds
- ✓ GridView displays all records
- ✓ No significant slowdown
- ✓ Can still interact normally

**Pass/Fail:** ___________

---

## Accessibility Testing

### Test: Keyboard Navigation
**Steps:**
1. Close mouse input
2. Use Tab key to navigate form fields
3. Use Shift+Tab to go backward
4. Use Enter to submit forms
5. Use Enter/Space on buttons

**Expected Results:**
- ✓ Can tab through all form fields
- ✓ Tab order logical
- ✓ Can submit form with Enter
- ✓ Button text visible with keyboard focus

**Pass/Fail:** ___________

### Test: Screen Reader Compatibility
**Steps:**
1. Test with screen reader (NVDA, JAWS, or VoiceOver)
2. Navigate the form
3. Read form labels
4. Activate buttons

**Expected Results:**
- ✓ Form labels read correctly
- ✓ Button purposes clear
- ✓ Table headers announced
- ✓ Error messages announced

**Pass/Fail:** ___________

---

## Error Message Testing

### Test: Required Field Error
**Steps:**
1. Try saving member without Full Name
2. Check error message

**Expected Results:**
- ✓ Clear error message displayed
- ✓ Message identifies missing field
- ✓ Form highlights error

**Pass/Fail:** ___________

### Test: Date Format Error
**Steps:**
1. Enter invalid date in event form
2. Try to save

**Expected Results:**
- ✓ Clear error message about date format
- ✓ Data not saved
- ✓ User can correct and retry

**Pass/Fail:** ___________

---

## Regression Testing

After any code changes, verify:
- [ ] All member CRUD operations work
- [ ] All event CRUD operations work
- [ ] Dashboard stats update correctly
- [ ] Form validation works
- [ ] Error messages display
- [ ] Session management intact
- [ ] Database transactions complete successfully
- [ ] GridView sorting/filtering still work (if implemented)

---

## Test Results Summary

| Test Area | Passed | Failed | Notes |
|-----------|--------|--------|-------|
| Member Add | | | |
| Member Edit | | | |
| Member Delete | | | |
| Member Validation | | | |
| Event Add | | | |
| Event Edit | | | |
| Event Delete | | | |
| Event Validation | | | |
| Dashboard Stats | | | |
| Session Management | | | |
| Data Persistence | | | |
| Browser Compat | | | |
| Responsive | | | |
| Accessibility | | | |
| Performance | | | |

**Overall Status:** ___________

**Test Date:** ___________

**Tester Name:** ___________

**Notes:**
```




```

---

**Last Updated**: 2026-06-07
