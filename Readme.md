## ХАКАТОН 296
Ссылка на бота:  

## Локальное развертывание
# Запуск БД
Запустить команду:
docker-compose up -d --build quick-postgres

# Запуск мигратора БД
Открыть docker-compose.yml

Изменить env ConnectionStrings__ApplicationDbContext в сервисе quick-migrator на актуальную строку подлючения к БД

Запустить команду:
docker-compose up -d --build quick-migrator

# Запуск API
Открыть docker-compose.yml

Изменить env ConnectionStrings__ApplicationDbContext в сервисе quick-api на актуальную строку подлючения к БД

Запустить команду:
docker-compose up -d --build quick-api

# Запуск UI
Изменить переменную окружения VITE_API_BASE_URL в файле ./Quick.UI/app/.env на актуальный URL API

Запустить команду:
docker-compose up -d --build quick-ui