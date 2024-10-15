# Fulfill3D Backend

- **App**: Visit https://fulfill3d.com to see the application.
- **Frontend**: Visit https://github.com/fulfill3d/Fulfill3D_Frontend to see the frontend repo.

Backend application for Fulfill3D

## Table of Contents

1. [Introduction](#introduction)
2. [Microservices](#microservices)
3. [Tech Stack](#tech-stack)

## Introduction

The CRM Application provides a platform for businesses to efficiently manage their operations across multiple locations. It facilitates the addition of stores, employee management, and service offerings. Clients can easily find and book services, ensuring a seamless experience.

## Microservices

### API
- **Fulfill3D.API.API:** Send form request, get posts, projects and about from Azure Cosmos DB

## Tech Stack

- **Backend:** .NET 8 (Isolated Worker), Azure Functions v4
- **Database:** Azure Cosmos DB
- **Authentication:** Google ReCaptcha to prevent bots on sending form request
- **Configuration & Secrets Management:** Azure App Configuration, Azure Key Vault
- **Hosting:** Microsoft Azure
