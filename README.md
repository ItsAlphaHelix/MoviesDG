# 🎬 **MoviesDG** 

- [https://moviesdg.devmania.click](https://moviedg.devmania.click)

![MoviesDG Homepage](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Homepage.png?raw=true)

## 📃 **Project Description**

### This is an application built using the Asp.Net Core technology and the MVC pattern. To use the application, each user must create their own profile. Once they log into the system, they will be able to watch trailers for their favorite movies and search for movies by genre, country, year, and actors. Each user will be able to create their own movie collection, add or remove movies from it. Additionally, the user of the application has a chat feature through which they can communicate with other users. The application also has a "Contacts" option that serves as a direct link to the support team. Each user has profile settings, including the ability to change their username, location, email, password, and enable two-factor authentication. The user can delete their profile if they decide to do so.

## 🌟 **Main Pages**

### 🎥 **All Movies**
![All Movies Page](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/All.png?raw=true)

### 🎬 **New Releases**
![New Releases Page](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/New-Releases.png?raw=true)

### 📚 **My Movies**
![My Movies Page](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/My-Movies.png?raw=true)

### 🎞️ **Movie Details (Personal Collection)**
![My Movie Details Page](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Mine-Movie-Details.png?raw=true)

### 🎥 **Movie Details (General)**
![Movie Details Page](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Movie-Details.png?raw=true)

### ✉️ **Contact Form**
![Contact Form Page](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Contact-Form.png?raw=true)

## 🔐 **Administration Pages**

### 👥 **All Users In Roles**
![Users in Roles](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/All-Users-In-Roles.png?raw=true)

### 🎬 **Create Movie**
![Create Movie](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Create-Movie.png?raw=true)

### ➕ **Add User to Role**
![Add User to Role](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Add-User-To-Role.png?raw=true)

### 📝 **All Submissions**
![All Submissions](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/All-Submisions.png?raw=true)

### 📝 **Submission Details**
![Submission Details](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Submision-Details.png?raw=true)

### ✉️ **Reply Message**
![Reply Message](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Reply-Message.png?raw=true)

### 🗑️ **Delete Account**
![Delete Account](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Delete-Personal-Data.png?raw=true)


## 🛠️ **Profile Settings Pages**

### 🔄 **Change Profile**
![Change Profile](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Change-Profile.png?raw=true)

### 📧 **Change Email**
![Change Email](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Change-Email.png?raw=true)

### 🔑 **Change Password**
![Change Password](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Change-Password.png?raw=true)

### 🔐 **Better Security (Enable Two-Factor Authentication)**
![Better Security](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Better-Security.png?raw=true)

## 🔐 **Identity**

### 📝 **Register**
![Register](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Register.png?raw=true)

### 🔑 **Login**
![Login](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Login.png?raw=true)

### ❓ **Forgot Password**
![Forgot Password](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Images/Forgot-Password.png?raw=true)

## 🛠️ **Built Using**

- ASP.NET Core 6.0
- Microsoft.AspNetCore.Identity
- MS SQL Server
- ASP.NET Core WebAPI
- Entity Framework Core 6.0
- Visual Studio 2022
- SendGrid
- Newtonsoft.Json
- TMDB API
- Repository Pattern
- NUnit & Mock
- HTML/CSS & JavaScript
- SignalR
- AJAX & JQuery
- QRCode.js
- Bootstrap & SCSS
- Font Awesome
- NToastNotify

## 🗂️ **Database Diagram**

![Database Diagram](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Database-Diagram.png?raw=true)


# Running the Application

Follow these steps to set up and run the application successfully:

## Step 1: Configure the Database Connection

1. **Open `appsettings.json` or `usersecrets.json`.**
2. **Set the database connection string:**

   - **For Server Authentication:**
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database={databaseName};User Id={serverName};Password={serverPassword};"
     }
     ```

   - **For Windows Authentication:**
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```

## Step 2: Import the Database

1. **Open SQL Server Management Studio.**
2. **Right-click on `Databases` and choose `Import Data-Tier Application...`** to import the provided [MoviesDGDatabase.bacpac](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/MoviesDGDatabase.bacpac) file.

## Step 3: Run the Application

1. **Start the application.**
2. **Log in using the following credentials:**

   - **Username:** Admin
   - **Password:** `admin23moviesdg@`

After completing these steps, the application should run properly.

## Additional Features

- **Email Functionality:**  
  To enable email sending capabilities, create an account in **Brevo** and obtain your SMTP credentials.

- **Adding New Movies:**  
  To add new movies to your database, create an account on **TMDB** and acquire your API key.

2. **Set the database connection string:**

  - **Here’s how the JSON configuration should look with all credentials:**

     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database={databaseName};Trusted_Connection=True;MultipleActiveResultSets=true"
       },
       "TMDB": {
         "ApiKey": "apiKey"
       },
       "BrevoSmtpSettings": {
         "Server": "brevoSmtpServer",
         "Port": 233,
         "Username": "username",
         "Password": "password"
       }
     }


Both services are free to use.

## 👤 **Author**

- **Dimitar Georgiev**  
  [LinkedIn Profile](https://www.linkedin.com/in/dimitar-georgiev-551a16242/)

![Author Image](https://github.com/ItsAlphaHelix/MoviesDG/blob/main/Asp.Net%20Advanced.jpg?raw=true)

## 📄 **License**

This project is licensed under the MIT License.  
[Read the License](https://choosealicense.com/licenses/mit/)
