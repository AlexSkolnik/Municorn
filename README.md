# Тестовое приложение для эмуляции отправки уведомлений.

## ДОПУЩЕНИЯ:
1. Миграция БД сделана в рантайме, т.к. это тестовый проект, чтобы удобне поднимать в докере.
2. На такой маленький проект с DDD заморачиватьcя смысла нет.
3. В задании не указано как работать с БД, через EF или Dapper, воспользуюсь удобством ORM.
4. Буду считать приложение внутренним, не требующим отдачи на фронт сообщений об ошибках.
	Обработку ситуаций, таких как дубль идентификаторов, запрос уведомления по несуществующему Id, делать не буду. 
	Все ошибки просто буду прокидывать в глобальный фильтр исключений и отдавать из контроллеров 500 со стектрейсом.
5. Так как не описано в ТЗ, по одому урлу или по разным будут приходить запросы на создание уведомлений для разных типов устройств, то решил сделать 2 версии контроллеров. 
	- V1 предолагает, что слаться уведомления для андроид и ios будут по разным урлам. Зато будет выполняться на входе в контроллер встроенная валидация, по урлу сразу понятно для кого уведомление.
	- V2 будет принимать уведомления по одному урлу для обоих типов устройств. Это потребует при передаче в контроллер модели уведомления дополнительного указания типа.

### Команды докера:
- docker build -t municorn-api . 
- docker run -d -p 5000:80 municorn-api
- docker rmi $(docker images -a -q)  -- удалить все образы
- docker-compose build
- docker-compose up