# HR Leaves Management Module

Full-stack test task implementation:

- Backend: ASP.NET Core 8 Web API, EF Core 8, Code First, SQL Server, Swagger
- Frontend: Angular standalone components, RxJS, responsive CSS, localStorage draft persistence

## Folder Structure

```text
backend/
  HRLeaveManagement.sln
  HrLeaves.Api/
    HrLeaves.Api.csproj
    Controllers/
    Data/
    DTOs/
    Migrations/
    Models/
    Services/
frontend/
  angular.json
  package.json
  src/
```

## Backend Setup

Open terminal in `backend` folder:

```bash
cd backend
dotnet restore
dotnet ef database update --project HrLeaves.Api
dotnet run --project HrLeaves.Api
```

Swagger will open at:

```text
https://localhost:5001/swagger
```

API base URL:

```text
https://localhost:5001/api
```

Default connection string is in:

```text
backend/HrLeaves.Api/appsettings.json
```

Change the SQL Server name if needed.

## Frontend Setup

Open another terminal in `frontend` folder:

```bash
cd frontend
npm install
npm start
```

Frontend URL:

```text
http://localhost:4200
```

## Sample API Request

```http
POST https://localhost:5001/api/leave-requests
Content-Type: application/json

{
  "employeeId": 1,
  "leaveTypeId": 1,
  "startDate": "2026-07-10",
  "endDate": "2026-07-14",
  "reason": "Family function"
}
```

## Implemented Features

- Leave request CRUD/listing
- Leave type management for HR
- Leave balances per employee
- StartDate <= EndDate validation
- Auto calculation of requested days excluding weekends
- Insufficient balance validation
- Overlapping request validation
- Pending approval list
- Approve/reject workflow
- Bulk approve/reject
- Balance deduction on approval
- No deduction on rejection
- Manual leave settlements
- Settlement history
- Dashboard filters
- CSV export
- Leave balance cards/progress bars
- Form draft persistence using localStorage
- Swagger documentation
- Code-first EF Core migrations for SQL Server

## Seed Data

The app seeds:

- Employee: Demo Employee
- Leave Types: Vacation, Sick, Maternity
- Leave Balances for Employee Id 1

Use employeeId `1` for testing.
