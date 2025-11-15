# 1. Билд-этап
FROM node:20-alpine AS build

# Рабочая директория
WORKDIR /app

# Копируем package.json и package-lock.json / yarn.lock
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем весь проект
COPY . .

# Собираем проект
RUN npm run build

# 2. Продакшен-этап
FROM nginx:alpine

# Копируем собранный билд в папку nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Копируем конфигурацию nginx (необязательно, можно оставить дефолт)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# Экспонируем порт
EXPOSE 80

# Запуск nginx
CMD ["nginx", "-g", "daemon off;"]