FROM quay.io/keycloak/keycloak:23.0.7 as builder

# Enable health and metrics support
ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true

# Configure a database vendor
ENV KC_DB=postgres

WORKDIR /opt/keycloak
# for demonstration purposes only, please make sure to use proper certificates in production instead
RUN keytool -genkeypair -storepass password -storetype PKCS12 -keyalg RSA -keysize 2048 -dname "CN=server" -alias server -ext "SAN:c=DNS:localhost,IP:127.0.0.1" -keystore conf/server.keystore
RUN /opt/keycloak/bin/kc.sh build

FROM quay.io/keycloak/keycloak:23.0.7
COPY --from=builder /opt/keycloak/ /opt/keycloak/

# change these values to point to a running postgres instance
ENV KC_DB=postgres
ENV KC_DB_PASSWORD=myStrongPassword123!
ENV KC_DB_SCHEMA=public
ENV KC_DB_URL_DATABASE=identity
ENV KC_DB_URL_HOST=micro-postgres
ENV KC_DB_USERNAME=postgres
ENV KC_HOSTNAME_ADMIN_URL=http://localhost:5050
ENV KC_HOSTNAME_URL=http://localhost:5050
ENV KEYCLOAK_ADMIN=berkslv
ENV KEYCLOAK_ADMIN_PASSWORD=myStrongPassword123!
ENV KEYCLOAK_LOGLEVEL=INFO
ENV KC_HTTP_ENABLED=true
EXPOSE 8080
ENTRYPOINT ["/opt/keycloak/bin/kc.sh", "start", "--optimized"]