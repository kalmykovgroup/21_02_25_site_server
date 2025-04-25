#!/bin/sh

: "${API_PORT:?❌ API_PORT не задан! Проверь переменные окружения.}" 
 
echo "🧹 Очищаем логи..."
find "/var/log/nginx/" -type f -name "*.log" -exec truncate -s 0 {} \;


echo "📁 Список /etc/nginx/conf.d до генерации:"
ls -l /etc/nginx/conf.d 
 
 
CONF_DIR="/etc/nginx/conf.d"
TEMPLATES="/etc/nginx/templates"
TEMPLATE_HTTP="$TEMPLATES/http.template" 
TARGET_CONF="$CONF_DIR/default.conf"

echo "API_PORT: $API_PORT" 

echo "🌐 NGINX entrypoint запущен..."

envsubst '${API_PORT}' < "$TEMPLATE_HTTP" > "$TARGET_CONF" 

echo "📁 Список /etc/nginx/conf.d после генерации:"
ls -l /etc/nginx/conf.d
echo "📄 Содержимое default.conf:"
cat /etc/nginx/conf.d/default.conf 

echo "🚀 Запуск nginx..."
nginx -g "daemon off;"
