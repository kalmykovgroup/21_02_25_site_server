#!/bin/bash

CERT_DIR=/certs
CERT_PFX=$CERT_DIR/cert.pfx
CERT_PASSWORD=$(cat /run/secrets/cert_password)

# Создаём сертификат только если он ещё не существует
if [ ! -f "$CERT_PFX" ]; then
  echo "[entrypoint] 🔐 Создание нового сертификата cert.pfx"
  openssl req -x509 -newkey rsa:4096 -keyout $CERT_DIR/key.pem -out $CERT_DIR/cert.pem \
    -days 365 -nodes -subj "/CN=dataprotection"

  openssl pkcs12 -export -out $CERT_PFX -inkey $CERT_DIR/key.pem -in $CERT_DIR/cert.pem \
    -passout pass:$CERT_PASSWORD

  rm $CERT_DIR/key.pem $CERT_DIR/cert.pem
fi

# Запускаем .NET приложение
exec "$@"
