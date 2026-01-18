# 🤖 AI HR Assistant - Backend API

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Gemini](https://img.shields.io/badge/Google%20Gemini-8E75B2?style=for-the-badge&logo=google&logoColor=white)

A powerful RESTful API that leverages Google's Gemini AI to provide intelligent HR assistance. Built with ASP.NET Core and Entity Framework, this backend handles employee queries, generates AI-powered responses, and maintains conversation history.

[Features](#-features) • [Tech Stack](#-tech-stack) • [Installation](#-installation--setup) • [API Documentation](#-api-endpoints) • [Contributing](#-contributing)

</div>

---

## ✨ Features

- 🧠 **AI-Powered Responses** - Integrates with Google Gemini 2.5 Flash for intelligent HR assistance
- 💬 **Conversation Management** - Complete CRUD operations for chat history
- 📊 **Database Persistence** - Reliable storage using Entity Framework Core and SQL Server
- 🔐 **Secure Configuration** - Environment-based settings management
- 🌐 **CORS Enabled** - Ready for cross-origin frontend integration
- ⚡ **High Performance** - Async/await patterns throughout
- 🛡️ **Error Handling** - Comprehensive exception handling and validation

---

## 🛠️ Tech Stack

| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core 10.0** | Web API Framework |
| **Entity Framework Core** | ORM for database operations |
| **SQL Server** | Relational database |
| **Google Gemini AI** | Natural language processing |
| **C# 12** | Programming language |

---

## 📋 Prerequisites

Before you begin, ensure you have the following installed:

- ✅ [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- ✅ [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- ✅ [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- ✅ [Google Gemini API Key](https://aistudio.google.com/app/apikey) (Free tier available)

---

## 🚀 Installation & Setup

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Huzaifa-mh/HRChatbotAPI.git
cd HRChatbotAPI
```

### 2️⃣ Configure Application Settings

Copy the example configuration file:

```bash
cp appsettings.Example.json appsettings.json
```

Edit `appsettings.json` with your settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=HRChatbotAPI;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "GeminiApiKey": "YOUR_GEMINI_API_KEY_HERE"
}
```

**Common SQL Server Names:**
- **SQL Server Express:** `YOUR_PC_NAME\SQLEXPRESS`
- **LocalDB:** `(localdb)\MSSQLLocalDB`

> ⚠️ **Security Note:** Never commit `appsettings.json` to version control. It's already in `.gitignore`.

### 3️⃣ Install Dependencies

```bash
dotnet restore
```

### 4️⃣ Apply Database Migrations

Using .NET CLI:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Using Visual Studio Package Manager Console:
```powershell
Add-Migration InitialCreate
Update-Database
```

### 5️⃣ Run the Application

```bash
dotnet run
```

The API will start at: **`https://localhost:7238`** (check console for exact URL)

---

## 📡 API Endpoints

### **Send Message**
```http
POST /api/chat
Content-Type: application/json

{
  "message": "Create a leave application for me"
}
```

**Response:**
```json
{
  "response": "Dear [Manager's Name],\n\nI am writing to formally request leave..."
}
```

---

### **Get Conversation History**
```http
GET /api/chat/history
```

**Response:**
```json
[
  {
    "id": 1,
    "userMessage": "Create a leave application",
    "aiResponse": "Dear Manager...",
    "timeStamp": "2025-01-11T10:30:00Z"
  }
]
```

---

### **Get Specific Conversation**
```http
GET /api/chat/{id}
```

**Response:**
```json
{
  "id": 1,
  "userMessage": "Create a leave application",
  "aiResponse": "Dear Manager...",
  "timeStamp": "2025-01-11T10:30:00Z"
}
```

---

## 📁 Project Structure

```
HRChatbotAPI/
├── 📂 Controllers/
│   └── ChatController.cs          # API endpoint handlers
├── 📂 Data/
│   └── HRChatbotDBContext.cs     # EF Core database context
├── 📂 Models/
│   ├── Conversation.cs            # Database entity
│   ├── ChatRequest.cs             # Request DTO
│   └── GeminiModels.cs            # Gemini API response models
├── 📂 Migrations/                 # EF Core migrations
├── 📄 Program.cs                  # Application entry point
├── 📄 appsettings.Example.json   # Configuration template
└── 📄 .gitignore                  # Git ignore rules
```

---

## 🧪 Testing with Postman

1. **Import Collection** - Use the endpoints documented above
2. **Set Headers** - Add `Content-Type: application/json` for POST requests
3. **Test POST** - Send a message and verify AI response
4. **Test GET** - Retrieve conversation history

### Sample Request

```bash
curl -X POST https://localhost:7238/api/chat \
  -H "Content-Type: application/json" \
  -d '{"message": "What is the sick leave policy?"}'
```

---

## 🔐 Security Best Practices

> **⚠️ NEVER commit sensitive data to version control!**

### Using User Secrets (Recommended for Development)

```bash
dotnet user-secrets init
dotnet user-secrets set "GeminiApiKey" "your-api-key-here"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

### Using Environment Variables (Production)

```bash
export GeminiApiKey="your-api-key"
export ConnectionStrings__DefaultConnection="your-connection-string"
```

---

## 🐛 Troubleshooting

### Database Connection Failed
- ✅ Verify SQL Server is running
- ✅ Check server name in connection string
- ✅ Ensure Windows Authentication is enabled

### Gemini API Errors

| Error Code | Issue | Solution |
|-----------|-------|----------|
| `404` | Model not found | Use `gemini-2.5-flash` or `gemini-2.0-flash` |
| `429` | Quota exceeded | Wait for reset or upgrade plan |
| `401` | Invalid API key | Verify key in Google AI Studio |

### Migration Issues
```bash
# Reset database
dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🎯 Future Enhancements

- [ ] Add authentication and authorization
- [ ] Implement rate limiting
- [ ] Add caching layer (Redis)
- [ ] Support for file uploads (PDF policies)
- [ ] Implement conversation threading
- [ ] Add unit and integration tests

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

---

## 📚 Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Guide](https://docs.microsoft.com/en-us/ef/core/)
- [Google Gemini API Docs](https://ai.google.dev/gemini-api/docs)
- [REST API Best Practices](https://restfulapi.net/)

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Muhammad Huzaifa**

[![GitHub](https://img.shields.io/badge/GitHub-Huzaifa--mh-181717?style=flat&logo=github)](https://github.com/Huzaifa-mh)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Connect-0077B5?style=flat&logo=linkedin)](https://www.linkedin.com/in/muhammadhuzaifamh)

---

## 🙏 Acknowledgments

- Google Gemini AI for powering intelligent responses
- Microsoft for the excellent .NET ecosystem
- The open-source community for inspiration

---

<div align="center">

**⭐ If you found this project helpful, please give it a star!**

Made with ❤️ by Muhammad Huzaifa

</div>
