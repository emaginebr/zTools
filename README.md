# zTools - Developer Utility API Toolkit

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/License-MIT-green)
[![NuGet](https://img.shields.io/nuget/v/zTools)](https://www.nuget.org/packages/zTools/)

## Overview

**zTools** is a RESTful API built with **.NET 8** that provides a collection of utility services for common development tasks: **ChatGPT/DALL-E integration**, **email delivery**, **S3-compatible file storage**, **Brazilian document validation (CPF/CNPJ)**, and **string manipulation utilities**.

The project follows **clean architecture** with separated layers and is available as a [NuGet package](https://www.nuget.org/packages/zTools/) for consuming the API from other .NET projects with strongly-typed clients and DTOs.

---

## 🚀 Features

- 🤖 **ChatGPT Integration** - Send messages, conversations, and custom requests to OpenAI's API
- 🎨 **DALL-E Image Generation** - Generate images with basic and advanced options
- 📧 **Email Service** - Send emails via MailerSend with email format validation
- 📁 **File Storage** - Upload and retrieve files from S3-compatible storage (DigitalOcean Spaces)
- 📄 **Document Validation** - Validate Brazilian CPF and CNPJ numbers with check digit verification
- 🔤 **String Utilities** - Slug generation, number extraction, and short unique ID generation
- 📦 **NuGet Package** - Consume the API from other .NET projects with typed clients and DTOs

---

## 🛠️ Technologies Used

### Core Framework
- **.NET 8.0** - Cross-platform framework for building web APIs
- **ASP.NET Core** - Web framework with Swagger/OpenAPI documentation

### Cloud & Storage
- **AWS SDK for S3 (4.0.6.10)** - S3-compatible storage (DigitalOcean Spaces, AWS S3)
- **MailerSend API** - Email delivery service

### AI & Image Processing
- **OpenAI API** - ChatGPT and DALL-E integration
- **SixLabors.ImageSharp (3.1.11)** - Image processing

### Additional Libraries
- **Newtonsoft.Json (13.0.3)** - JSON serialization
- **Stripe.NET (48.5.0)** - Payment processing
- **Swashbuckle.AspNetCore (9.0.4)** - Swagger/OpenAPI docs
- **NoobsMuc.Coinmarketcap.Client (3.1.1)** - Cryptocurrency data

### Testing
- **xUnit (2.5.3)** - Unit testing framework
- **Moq (4.20.70)** - Mocking library
- **RichardSzalay.MockHttp (7.0.0)** - HTTP client mocking

### DevOps
- **Docker** - Multi-stage containerization
- **GitHub Actions** - CI/CD with automated testing, deployment, and NuGet publishing
- **GitVersion** - Semantic versioning

---

## 📁 Project Structure

```
zTools/
├── .github/workflows/           # CI/CD pipelines
│   ├── deploy-prod.yml          # Production deployment via SSH + Docker
│   ├── publish-nuget.yml        # NuGet package publishing
│   └── version-tag.yml          # Semantic version tagging
├── zTools.API/                  # API Layer
│   ├── Controllers/             # 5 REST controllers
│   │   ├── ChatGPTController    # AI text & image generation
│   │   ├── DocumentController   # CPF/CNPJ validation
│   │   ├── FileController       # S3 file operations
│   │   ├── MailController       # Email & validation
│   │   └── StringController     # String utilities
│   ├── Program.cs               # Host configuration
│   └── Startup.cs               # Middleware & DI setup
├── zTools.Application/          # DI wiring layer
│   └── Initializer.cs           # Service registration
├── zTools.Domain/               # Business logic layer
│   ├── Services/                # Service interfaces & implementations
│   │   ├── ChatGPTService       # OpenAI API client
│   │   ├── FileService          # S3 storage client
│   │   └── MailerSendService    # Email delivery client
│   └── Utils/                   # Static utility classes
│       ├── DocumentoUtils       # CPF/CNPJ validation
│       ├── EmailValidator       # Email format validation
│       ├── ShuffleEx            # Fisher-Yates shuffle extension
│       ├── SlugHelper           # URL-friendly slug generation
│       └── StringUtils          # Number extraction & unique IDs
├── zTools.Tests/                # Test suite
│   ├── ACL/                     # Anti-corruption layer tests
│   └── Domain/                  # Service & utility tests
├── zTools/                      # NuGet package (ACL + DTO)
├── Dockerfile                   # Multi-stage build
├── docker-compose.yml           # Development environment
├── docker-compose-prod.yml      # Production environment
├── .env.example                 # Environment variable template
└── GitVersion.yml               # Semantic versioning config
```

### Ecosystem

| Project | Type | Package | Description |
|---------|------|---------|-------------|
| **[zTools API](https://github.com/landim32/NTools.API)** | REST API | - | This project — utility API server |
| **[zTools](https://www.nuget.org/packages/zTools/)** | NuGet | [![NuGet](https://img.shields.io/nuget/v/zTools)](https://www.nuget.org/packages/zTools/) | ACL clients & DTOs for consuming the API |

#### Dependency Graph

```
zTools.API (REST API)
├── zTools.Application → DI registration
├── zTools.Domain      → Business logic & services
│   └── zTools         → ACL clients & DTOs (NuGet)
└── zTools             → ACL clients & DTOs (NuGet)
```

> **Want to consume this API from another .NET project?** Install the [zTools](https://www.nuget.org/packages/zTools/) package to get strongly-typed clients and DTOs with full IntelliSense support.

---

## ⚙️ Environment Configuration

### 1. Copy the environment template

```bash
cp .env.example .env
```

### 2. Edit the `.env` file

```bash
# MailerSend Configuration
MAILERSEND_MAILSENDER=your-email@example.com
MAILERSEND_APIURL=https://api.mailersend.com/v1/email
MAILERSEND_APITOKEN=your-mailersend-api-token

# ChatGPT Configuration
CHATGPT_APIKEY=your-openai-api-key
CHATGPT_APIURL=https://api.openai.com/v1/chat/completions
CHATGPT_MODEL=gpt-5.2
CHATGPT_IMAGEAPIURL=https://api.openai.com/v1/images/generations

# DigitalOcean Spaces (S3) Configuration
S3_ACCESSKEY=your-digitalocean-spaces-access-key
S3_SECRETKEY=your-digitalocean-spaces-secret-key
S3_ENDPOINT=https://your-space-name.nyc3.digitaloceanspaces.com
```

⚠️ **IMPORTANT**:
- Never commit the `.env` file with real credentials
- Only the `.env.example` should be version controlled
- Change all default values before deployment

---

## 🐳 Docker Setup

### Quick Start

```bash
# Build and start
docker compose up -d --build

# View logs
docker compose logs -f ztools-api

# Stop
docker compose down
```

### Production Deployment

```bash
docker compose -f docker-compose-prod.yml up -d --build
```

### Accessing the API

| Service | URL |
|---------|-----|
| **API Base** | http://localhost:5001 |
| **Swagger UI** | http://localhost:5001/swagger |
| **Health Check** | http://localhost:5001/ |

### Docker Compose Commands

| Action | Command |
|--------|---------|
| Start services | `docker compose up -d` |
| Start with rebuild | `docker compose up -d --build` |
| Stop services | `docker compose stop` |
| View status | `docker compose ps` |
| View logs | `docker compose logs -f` |
| Remove containers | `docker compose down` |
| Full rebuild (no cache) | `docker compose build --no-cache && docker compose up -d` |

---

## 🔧 Manual Setup (Without Docker)

### Prerequisites
- .NET 8.0 SDK
- MailerSend API account
- OpenAI API account
- DigitalOcean Spaces (or S3-compatible storage) account

### Setup Steps

#### 1. Restore dependencies

```bash
dotnet restore
```

#### 2. Build the project

```bash
dotnet build
```

#### 3. Run the API

```bash
dotnet run --project zTools.API
```

The API will be available at **https://localhost:9001/swagger**.

---

## 🧪 Testing

### Running Tests

**All tests:**
```bash
dotnet test
```

**With coverage:**
```bash
dotnet test --configuration Release --collect:"XPlat Code Coverage"
```

**Single test class:**
```bash
dotnet test --filter "FullyQualifiedName~zTools.Tests.Domain.Utils.SlugHelperTest"
```

**Single test method:**
```bash
dotnet test --filter "FullyQualifiedName~zTools.Tests.Domain.Utils.SlugHelperTest.TestGenerateSlug"
```

### Test Structure

```
zTools.Tests/
├── ACL/                    # Anti-corruption layer client tests
└── Domain/
    ├── Services/           # ChatGPT, FileService, MailerSend tests
    └── Utils/              # SlugHelper, StringUtils, DocumentUtils,
                            # EmailValidator, ShuffleEx tests
```

**Test patterns:** `[Fact]` for single cases, `[Theory]` with `[InlineData]` for parameterized tests. Uses `Moq` for dependency mocking and `RichardSzalay.MockHttp` for HTTP client testing.

---

## 📚 API Documentation

Interactive documentation is available via **Swagger UI** at `/swagger` when running the API.

### ChatGPT Controller (`/ChatGPT`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/ChatGPT/sendMessage` | Send a single message to ChatGPT |
| POST | `/ChatGPT/sendConversation` | Send multi-message conversation |
| POST | `/ChatGPT/sendRequest` | Send custom request with full parameters (model, temperature, max tokens) |
| POST | `/ChatGPT/generateImage` | Generate image with DALL-E |
| POST | `/ChatGPT/generateImageAdvanced` | Generate image with advanced DALL-E options |

**Example — Send Message:**
```json
POST /ChatGPT/sendMessage
{
  "message": "What is the capital of France?"
}
// Response: "The capital of France is Paris."
```

**Example — Send Conversation:**
```json
POST /ChatGPT/sendConversation
[
  { "role": "system", "content": "You are a helpful assistant." },
  { "role": "user", "content": "Explain REST APIs" }
]
```

**Example — Custom Request:**
```json
POST /ChatGPT/sendRequest
{
  "model": "gpt-4o",
  "messages": [
    { "role": "user", "content": "Hello" }
  ],
  "temperature": 0.7,
  "maxCompletionTokens": 500
}
```

### Mail Controller (`/Mail`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Mail/sendMail` | Send email via MailerSend |
| GET | `/Mail/isValidEmail/{email}` | Validate email format |

**Example — Send Email:**
```json
POST /Mail/sendMail
{
  "to": "recipient@example.com",
  "subject": "Test Email",
  "from": "sender@example.com",
  "html": "<h1>Hello World</h1>"
}
```

### File Controller (`/File`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/File/{bucketName}/getFileUrl/{fileName}` | Get public URL of a stored file |
| POST | `/File/{bucketName}/uploadFile` | Upload file to S3 bucket (100MB limit) |

**Example — Upload File:**
```
POST /File/my-bucket/uploadFile
Content-Type: multipart/form-data
file: [binary data]
```

### Document Controller (`/Document`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Document/validarCpfOuCnpj/{cpfCnpj}` | Validate Brazilian CPF or CNPJ |

**Example:**
```
GET /Document/validarCpfOuCnpj/12345678900
// Response: true/false
```

### String Controller (`/String`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/String/generateSlug/{name}` | Convert text to URL-friendly slug |
| GET | `/String/onlyNumbers/{input}` | Extract only numeric characters |
| GET | `/String/generateShortUniqueString` | Generate short unique Base62 ID |

**Examples:**
```
GET /String/generateSlug/Hello World 123     → "hello-world-123"
GET /String/onlyNumbers/abc123def456         → "123456"
GET /String/generateShortUniqueString        → "x4k9p2"
```

---

## 🔄 CI/CD

### GitHub Actions Workflows

#### Deploy to Production (`deploy-prod.yml`)
- **Triggers:** Push to `main`, manual dispatch
- **Steps:**
  1. Run tests (restore → build → test)
  2. SSH into production server
  3. Pull latest code
  4. Inject secrets from GitHub Secrets
  5. Build and deploy with Docker Compose

#### Version Tagging (`version-tag.yml`)
- **Triggers:** Push to `main`
- **Steps:** Runs GitVersion to create semantic version tags

#### Publish NuGet (`publish-nuget.yml`)
- **Triggers:** After successful version tagging
- **Steps:** Build, pack, and publish `zTools` package to NuGet.org

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Development Setup

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Make your changes
4. Run tests (`dotnet test`)
5. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
6. Push to the branch (`git push origin feature/AmazingFeature`)
7. Open a Pull Request

---

## 👨‍💻 Author

Developed by **[Rodrigo Landim](https://github.com/landim32)**

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/landim32/NTools.API/issues)

---

**⭐ If you find this project useful, please consider giving it a star!**
