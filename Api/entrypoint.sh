#!/bin/bash


: "${CERTS_DIR:?❌ CERTS_DIR не задан! Проверь переменные окружения.}" 
: "${CERT_PASSWORD_FILE:?❌ CERT_PASSWORD_FILE не задан!}"

if [ ! -f "$CERT_PASSWORD_FILE" ]; then
  echo "❌ Файл с паролем для сертификата не найден: $CERT_PASSWORD_FILE"
  exit 1
fi
 
CERT_PASSWORD=$(cat ${CERT_PASSWORD_FILE})

CERT_PATH=$CERTS_DIR/cert.pem
KEY_PATH=$CERTS_DIR/key.pem

# Генерация ключа и сертификата только если их ещё нет
if [ ! -f "$CERT_PATH" ] || [ ! -f "$KEY_PATH" ]; then
  echo "[entrypoint] 🔐 Генерация PEM-сертификата"

  # Приватный ключ с паролем
  openssl genrsa -aes256 -passout pass:$CERT_PASSWORD -out $KEY_PATH 4096

  # Публичный сертификат
  openssl req -new -x509 -key $KEY_PATH -passin pass:$CERT_PASSWORD \
    -out $CERT_PATH -days 365 -subj "/CN=dataprotection"
fi

# Запуск dotnet
exec "$@"
