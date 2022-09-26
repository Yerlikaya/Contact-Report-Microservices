# RiseTechnology
Contact&amp;Report Microservices

PROJE HAKKINDA

**Contact Servisinde MongoDB kullanıldı. Büyük datalarda hız katacağı ve json veri yapısı sebebiyle tercih edildi.

**Report Servisinde PostgreSQL EF Core CodeFirst ile kulanıldı ve migration dosyaları oluşturuldu. 

**Ayrıca Report api üzerinde bir rapor create edildiğinde event bazlı RabbitMQ da report-queue adındaki kuyruğa o anki rapor kaydına özel bir kayıt bırakıldı.

**Kuyruk ExcelBacgroundSerivice ile dinlenerek kuyruktan veri aldındığında Contact Apisine bir rest isteği atıldı ve tüm contactlar ve altındaki
communicationlar BackgroundService tarafından alınarak kuruğa bırakılan uniq değer ile wwwRoot dizininde bir Excel file yaratıldı.

**Bu işlemin durumuna göre ReportDB deki ilgili kaydın durumu WAITING, INPROGRESS, COMPLETED ve FAILED durumlarına göre güncellendi.

**DataBaseler Docker ile kaldırılıp kullanılmıştır.

**UnitTestler e oluşturacak kadar zamanım kalmadığı için bir şey yapamadım. 

**RabbitMQ Cloud üzerinden kullanılmıştır ve bilgileri aşağıdadır. 

Teşekkürler...

Oğuzhan Yerlikaya


RabbitMQ Cloud Email: wahiba6087@ploneix.com
Password: 6WLH.X22WFnDWCu
