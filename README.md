# Yan Koltuk

Yan Koltuk is a platform that enables users to organize and share safe and economical journeys.

## 📌 About the Project
Yan Koltuk is a web and mobile application developed to facilitate student tracking in school buses. Parents can monitor whether their children have boarded or left the bus, and bus service drivers/stewardess' can change the registered students statuses while managers can organize bus services.

## 🚀 Features
- **Admin Panel**: Administrators can manage services and users.
- **Parent Tracking**: Parents can view their children's bus movements.
- **Driver Management**: Bus drivers can view student lists and take attendance.
- **Real-Time Tracking**: Bus routes and student movements can be monitored.
- **Reporting**: Detailed reports on service history can be downloaded.

## 🛠️ Technologies Used
**Backend**: ASP.NET Core, Microsoft SQL Server (MSSQL)  
**Frontend**: React.js (Web), Flutter (Mobile)  
**Others**: Google Maps API, Azure, Apache

## 💻 Setup
### Backend
```bash
# Clone the repository
git clone https://github.com/koixos/YanKoltuk.git
cd YanKoltuk/YanKoltukBackend

# Install dependencies
dotnet restore

# Run database migrations
dotnet ef database update

# Start the application
dotnet run
```

### Frontend (Web - React.js)
```bash
cd uis/web
npm install
npm start
```

### Mobile (Flutter)
```bash
cd uis/mobile
flutter pub get
flutter run
```

## 📽️ Demo
Check out the YouTube demo videos to see how Yan Koltuk works on mobile and web. Please note that these are not the final version of the project. (These demos do not include the latest version of the project)
- [Demo video for web](https://youtu.be/wrKYUZhz1eA?si=zj4rhCeTbwYyhzoM)
- [Demo video for mobile](https://youtu.be/1U0kIp4myMY?si=6Yz_3b7U9S8m3GHb)

## 📜 License
This project is licensed under the MIT License.

---
**📬 Contact:** 
[GitHub](https://github.com/koixos/YanKoltuk) | [LinkedIn](https://linkedin.com/in/sumeyyezeynepgurbuz)

