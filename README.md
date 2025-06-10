# DocumentIngestionService

## About
This is a relatively simple demonstration application which should be enough to showcase theoretically a real world scenario.  
I've used some inspirations both from my past experience working with something similar, and some personal projects I've worked on. 
For preparation I have used Articles, videos and sparring with chatgpt, regarding some of the design decisions, pros and cons of some architectures, etc.
- [How to Run](#how-to-run)
- [Architecture](#architecture)
- [Tests](#tests)


---

## Endpoints
The application exposes two main endpoints:
* POST /invoices to submit invoice data
* GET /invoices/{id} to retrieve a specific invoice

##

## How to run: 

### Navigate to source location:
cd src/DocumentIngestion.Api

### Restore dependencies 
dotnet restore

### Run the application:
dotnet run

### Project is available with swagger:
https://localhost:7227/swagger/index.html

### or postman url
* https://localhost:7227/invoices (For post requests)
* https://localhost:7227/invoices/guid (For getbyid request)

### For tests:
Navigate to solution root
cd path/to/DocumentIngestionService

### Run:
dotnet test

---

## Architecture

---

## About
This is a relatively simple demonstration application which should be enough to showcase theoretically a real world scenario.  
I've used some inspirations both from my past experience working with something similar, and some personal projects I've worked on. 
For preparation I have used Articles, videos and sparring with chatgpt, regarding some of the design decisions, pros and cons of some architectures, etc.

The app simply receives a post request and a get request, I picked minimal API because I believed it would make sense in a smaller more contained project, since then
I might have some reservations, mostly just because the functionality for data annotations seems to be missing, not a big deal for this project, but it's something to keep in mind
