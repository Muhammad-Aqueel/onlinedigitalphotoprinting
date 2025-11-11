# Online Digital Photo Printing - ASP.NET Core Web Application

## Project Overview

Digital Photo Printing is a full-featured web application built with ASP.NET Core 7.0 that provides online photo printing services. The application supports user registration, authentication, order management, and administrative functions.

## Features

### Authentication & Authorization
- **ASP.NET Core Identity** integration
- Role-based access control (Admin and User roles)
- Custom user registration with extended profile fields
- Secure login/logout functionality
- Password reset and management

### Order Management
- Create, view, edit, and delete photo printing orders
- File upload for images with folder organization
- Multiple print size options (4x6, 10x12, 6x4, 12x10)
- Order status tracking (pending, completed, etc.)
- Credit card information handling

### User Roles
- **Admin**: Full access to all orders, user management, system configuration
- **User**: Personal order management, profile customization

### User Interface
- Responsive design with dark/light theme toggle
- Modern photography-themed layout
- Custom CSS styling and JavaScript interactions
- Mobile-friendly navigation

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 7.0
- **Authentication**: ASP.NET Core Identity
- **Database**: SQL Server with Entity Framework Core
- **Architecture**: MVC Pattern with Razor Pages

### Frontend
- **Styling**: Bootstrap CSS framework
- **Icons**: Font Awesome
- **JavaScript**: jQuery with custom scripts
- **Gallery**: LightGallery for image display

## Key Models

### ApplicationUser
- Extends IdentityUser with additional fields:
  - First Name, Last Name
  - Date of Birth
  - Gender
  - Address
  - Profile Picture

### PurchaseOrder
- Order management with fields:
  - Order Number (Primary Key)
  - Image Title and Name
  - Folder Name (user-specific)
  - Print Size
  - Email and Contact Information
  - Credit Card Details
  - Order Status

## Database Configuration

- **Database**: SQL Server LocalDB
- **Migrations**: Entity Framework Core code-first approach
- **Seeding**: Automatic admin user and role creation on startup

## Setup Instructions

### Prerequisites
- .NET 7.0 SDK
- SQL Server Express LocalDB
- Visual Studio 2022 or VS Code

### Installation Steps
1. Clone or extract the project files
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Update database:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

### Default Admin Account
- **Email**: admin@gmail.com
- **Password**: Admin@123

## Key Features in Detail

### Order Processing
- Users can upload images for printing
- Multiple print size options available
- Orders are organized in user-specific folders
- Admin can manage and update order status

### Security Features
- Password hashing and validation
- Role-based authorization
- Secure file upload handling
- CSRF protection

### User Experience
- Responsive design for all devices
- Dark/Light theme persistence using cookies
- Intuitive navigation with off-canvas menu
- Form validation and error handling

## Customization

### Styling
- Theme switching functionality
- Bootstrap customization

### Configuration
- Connection strings in `appsettings.json`
- Identity settings in `Program.cs`
- Role management in `DbSeeder.cs`

## Browser Support
- Modern browsers with HTML5 and CSS3 support
- Responsive design for mobile devices
- JavaScript-enabled required for full functionality

---

This application provides a complete solution for digital photo printing services with secure user management and order processing capabilities.