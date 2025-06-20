# Order Management API

A simple .NET Core Web API for managing customer orders, calculating discounts, and providing order analytics. Designed with clean architecture and extensibility in mind, this solution contains both the API and its associated test project.

## Features

Manage customer orders and statuses (Pending → Delivered)
-Calculate order discounts based on customer segment and order history
- Generate analytics (average order value, fulfillment time, total orders)
-Unit tested services with business rules and transitions

### Technologies Used

- ASP.NET Core Web API
- C# 10 / .NET 6 or later
- Swashbuckle (Swagger) for API docs
- xUnit (or NUnit) for testing

---
#### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- Visual Studio 2022 or later

  
##### RUnning
1. **Clone the repo**:

   git clone https://github.com/bernardadero/OrderManagement.API.git
   cd OrderManagement.API
   
3. Open in Visual Studio:
    Open OrderManagement.sln
   Set OrderManagement.API as the startup project
   Press F5 to run

4. Test the API in Swagger:

Navigate to: https://localhost:<port>/swagger

###### API Endpoints

1. Update Order Status
http
Copy
Edit
PUT /api/orders/{id}/status
Body: "Processing", "Shipped", "Delivered", "Cancelled"

2. Get Order Analytics
http
Copy
Edit
GET /api/orders/analytics
Response: 
  "averageOrderValue": 1050.50,
  "averageFulfillmentTime": "01:45:30",
  "totalOrders": 15

---------

####### Comments
I have uploaded the root folder to preserve project structure
I decided to create one solution with two projects  
1. OrderManagement.API/
2.OrderManagement.Tests



