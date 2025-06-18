# DocumentIngestionService

rdme docs (for fun)
https://test-mrsb.readme.io/reference/createinvoice#/

## Table of Content
- [How to Run](#how-to-run)
- [Architecture](#architecture)
- [Dependabot](#dependabot)
- [CI/CD](#cicd)
- [Commits and Rules](#branches-and-commit-rules)
- [Security / CodeQL](#security)

## How to run: 

### Navigate to source location:
cd src/DocumentIngestion.Api (src is where the solution file is located)

### Restore dependencies 
dotnet restore

### Run the application:
dotnet run --launch-profile https 

### Project is available with swagger:
* http://localhost:5128/swagger/index.html
* https://localhost:7227/swagger/index.html

### or postman url
* http://localhost:5128/invoices  (For post requests)
* http://localhost:5128/invoices/guid (For getbyid request)
* https://localhost:7227/invoices (For post requests)
* https://localhost:7227/invoices/guid (For getbyid request)

### Request example for post

```
{
  "invoiceNumber": "string",
  "supplierId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "invoiceDate": "2025-06-10T08:07:27.128Z",
  "currency": "string",
  "lines": [
    {
      "description": "string",
      "quantity": 0,
      "unitPrice": 0
    }
  ]
}
```

### For tests:
Navigate to solution root
cd path/to/DocumentIngestionService

### Run:
dotnet test

---

## Endpoints
The application exposes two main endpoints:
* POST /invoices to submit invoice data
* GET /invoices/{id} to retrieve a specific invoice

## Dependabot

![Dependabot](https://img.shields.io/badge/Dependabot-enabled-brightgreen)

This project uses **Dependabot** to keep dependencies up to date:

- Weekly updates for NuGet packages  
- Monthly check for the .NET SDK  
- Pull requests created automatically after passing CI  
- Just tag `@dependabot rebase` or `@dependabot merge` to manage updates

## Cicd

[![.NET CI](https://github.com/kan-1337/DocumentIngestionService/workflows/.NET%20CI/badge.svg)](https://github.com/kan-1337/DocumentIngestionService/actions/workflows/dotnet-ci.yml)




### CI/CD 

Builds project, Runs automatic tests, both unit and integration tests.  Make sure we need to confirm PRs, have others look at them, etc. 

## Inspirations

Here are some links and articles for best practices, and examples, including my own git repo from a small project I've made.

###Links

[MicroService architecture explained](https://vfunction.com/blog/net-microservices-architecture/)

[Best practice](https://medium.com/%40xperturesolutions/best-practices-for-microservices-in-net-cc3005803005)

[.NET 9: New Features and Best Practices](https://dev.to/leandroveiga/enhanced-microservices-support-in-net-9-new-features-and-best-practices-1ci2#:~:text=Conclusion-,.,%2C%20secure%2C%20and%20resilient%20microservices.)

[My old project](https://github.com/kan-1337/MicroServices)

### Branches and Commit rules

Dev is now the default branch, if this would ever go to production, the goal would be to make master/main branch the final step to update production.  
So for now, all branches must inherit or be created from Dev.  

At least one reviewer is required to pass the PR, we also need the tests and sync to readme docs to finish building before everything is green and good to go. 

I have also disabled force push, only admin can bypass these rules.

### Workflow

Example: 
- Create branch from dev for your feature/fix.
- Open a Pull Request targeting dev.
- Review & approval required (can be self-approval if solo, but ideally by another contributor).
- Merge to dev after all checks pass.
- Promote to master via PR when ready for production release.

### Security

[![CodeQL](https://img.shields.io/github/workflow/status/kan-1337/DocumentIngestionService/CodeQL)](https://github.com/kan-1337/DocumentIngestionService/actions/workflows/codeql-analysis.yml)



This project leverages GitHub’s security features and CodeQL analysis to proactively detect and address potential vulnerabilities:

- Code Scanning with CodeQL: Every pull request and push to dev or master triggers a CodeQL scan, automatically identifying common security issues in your C# codebase.
- Dependabot: Monitors dependencies for known vulnerabilities and keeps packages up to date.
- Branch Protection Rules: Ensures that critical branches (such as dev and master) require pull request reviews and passing status checks before merging.

### Enabling CodeQL Locally

    gh extension install github/gh-codeql
    gh codeql analyze --database <db-path>

---

## Architecture
![Architecture Diagram](https://github.com/kan-1337/DocumentIngestionService/blob/master/DocumentIngestionArchitectureDiagram.jpg)
---

## About
This is a relatively simple demonstration application which should be enough to showcase theoretically a real world scenario.  
I've used some inspirations both from my past experience working with something similar, and some personal projects I've worked on. 
For preparation I have used Articles, videos and sparring with chatgpt, regarding some of the design decisions, pros and cons of some architectures, etc.

The app simply receives a post request and a get request, I picked minimal API because I believed it would make sense in a smaller more contained project, since then
I might have some reservations, mostly just because the functionality for data annotations seems to be missing, not a big deal for this project, but it's something to keep in mind
