# DocumentIngestionService

##

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

### For tests:
Navigate to solution root
cd path/to/DocumentIngestionService

### Run:
dotnet test

##

## About
This is a relatively simple demonstration application which should be enough to showcase theoretically a real world scenario.  
I've used some inspirations both from my past experience working with something similar, and some personal projects I've worked on. 
For preparation I have used Articles, videos and sparring with chatgpt, regarding some of the design decisions, pros and cons of some architectures, etc.

The app simply receives a post request and a get request, I picked minimal API because I believed it would make sense in a smaller more contained project, since then
I might have some reservations, mostly just because the functionality for data annotations seems to be missing, not a big deal for this project, but it's something to keep in mind

The application exposes two main endpoints:
* POST /invoices to submit invoice data
* GET /invoices/{id} to retrieve a specific invoice


##

## Inspirations

Here are some links and articles for best practices, and examples, including my own git repo from a small project I've made.

###Links
[MicroService architecture explained](https://vfunction.com/blog/net-microservices-architecture/)

[Best practice](https://medium.com/%40xperturesolutions/best-practices-for-microservices-in-net-cc3005803005)

[.NET 9: New Features and Best Practices](https://dev.to/leandroveiga/enhanced-microservices-support-in-net-9-new-features-and-best-practices-1ci2#:~:text=Conclusion-,.,%2C%20secure%2C%20and%20resilient%20microservices.)

[My old project](https://github.com/kan-1337/MicroServices)

