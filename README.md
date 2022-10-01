# RiseTechnology
_Contact&amp;Report Microservices_



## Contact Microservice
- **MongoDB** database ini kullanan bu serviste veriler **Contact** ve **Communication** collection larında tutulur.

- Communication Collection -> **Id,** enum **CommunicationType{_EMAIL, PHONE, LOCATION_}, Address, ContactId** bilgisini içeririr.

- Contact Collection -> **Id, FirstName, LastName, Company** bilgisini içerirken ilgili **ContactId** ile kayıtlı Communication lar kod tarafında modele eklenir.

- Bu collection lar için ayrı ayrı controller içinde(ContactController, CommunicationController) crud işlemleri yapılırken **ContactController** da ek olarak 
**ReportService** tarafından oluşturulacak olan excel in verisini çekebileceği **GetContactStatistics** end pointi eklenmiştir.

- **GetContactStatistics** kısaca her bir Location için o communication bilgisine sahip contact sayısını ve yine o locationdaki contact ların PHONE tipindeki
communication sayısını döndürecektir.


## Report Microservice
- PostgreSQL database ini kullanan bu serviste veriler Report tablosunda tutulur.

- Report table -> **Id, CreatedDate, enum Status{WAITING, INPROGRESS, COMPLETED, FAIL}, FilePath** bilgisini içerir.

- **ReportController** ile temel crud işlemleri gerçekleştirilir.

- Report Create edilirken hiç bir bilgiye gerek yoktur. **CreateDate = DateTime.Now, Status = WAITING, FilePath = "NoPath"** olarak bir kayıt yaratılır.

- Report Microservice ContactStatictics bilgisini excel e aktarmak için RabbitMQ kullanır.

- RabbitMQ veriyi taşımak yerine event göndererek BackgroundService i tetiklemektedir. Tetiklenen BackgroundService **httpClient** ile **ContactMicroservice** ine istek
atarak **GetContactStatistics** verisini getirecektir. Daha sonra bu verilerden bir **yyyy-MM-dd-HH-mm-ss_GUUD.xlsx** formatında bir excel dosyası oluşturulacak ve
projedeki **wwwRoot** klasörüne kaydedilecektir. **Report** kaydının **FilePath** i ve **Status** ü işlem durumuna göre **INPROGRESS, COMPLETED, FAIL** durumlarıyla
güncellenecektir.

- **Docker Compose** tanımı yapılmış ve bu yöntem ile çalıştırıldığında oluşan excel dosyalarının **VOLUME** ü  **C:\Users\Public\Documents\ContactStatistics** 
olarak gösterilmiştir.

- **Contact** ve **Report** porjeleri için **UnitTest** projeleri ana dizinde UnitTest klasörü altında ayrı ayrı tamamlanmıştır.

## ##
### Oğuzhan Yerlikaya ###
