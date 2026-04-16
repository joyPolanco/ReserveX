# ReserveX Backend

## 1. Introducción

ReserveX es un backend orientado a la gestión de reservas, diseñado bajo principios de arquitectura limpia y orientado a dominios complejos donde intervienen disponibilidad, concurrencia, pagos y procesamiento asincrónico.

El sistema está enfocado en garantizar consistencia de datos, control de concurrencia, y separación clara de responsabilidades.

---

## 2. Enfoque Arquitectónico

El sistema adopta los siguientes principios:

* Clean Architecture
* Domain-Driven Design (DDD)
* CQRS (Command Query Responsibility Segregation)

### 2.1 Capas del sistema

* **Domain**: Contiene las entidades, reglas de negocio y lógica central.
* **Application**: Implementa los casos de uso mediante CQRS.
* **Infrastructure**: Persistencia, mensajería, servicios externos.
* **API**: Exposición de endpoints REST documentados con Swagger.

---

## 3. Patrones Implementados

### 3.1 CQRS

Separación estricta entre:

* **Commands**: Modifican el estado del sistema.
* **Queries**: Consultan datos sin efectos secundarios.

### 3.2 Result Pattern

Estandariza las respuestas:

* Éxito
* Error
* Validaciones

Evita el uso excesivo de excepciones para control de flujo.

### 3.3 Repository y Unit of Work

* **Repository**: Abstracción del acceso a datos.
* **Unit of Work**: Control de transacciones y consistencia.

### 3.4 Manejo Global de Excepciones

* Captura centralizada
* Logging estructurado
* Respuestas homogéneas

---

## 4. Seguridad

### 4.1 Autenticación

* JWT Access Tokens
* Refresh Tokens persistidos

### 4.2 Rotación de Tokens

* Cada refresh token genera uno nuevo
* El anterior se marca como revocado
* Prevención de reutilización

### 4.3 Expiración

* Tokens con fecha límite
* Control de revocación manual

---

## 5. Modelo de Roles

* **Admin**: Control completo del sistema
* **Manager**: Gestión de estaciones
* **Provider**: Gestión de recursos
* **User**: Creación de reservas

---

## 6. Dominio del Negocio

### 6.1 Estaciones

Representan unidades físicas o lógicas donde se realizan reservas.

### 6.2 Recursos

Elementos reservables asociados a estaciones.

### 6.3 Horarios

Definidos por día de la semana mediante `StationSchedule`.

### 6.4 Excepciones

Permiten modificar disponibilidad por fechas específicas.

---

## 7. Generación de Slots

Los slots son generados en base a:

* Horarios semanales
* Excepciones
* Recursos asociados

Se generan considerando:

* Duración del slot
* Capacidad
* Precio base

**Ubicación del diagrama:**

```
/docs/images/slot-generation.png
```

---

## 8. Gestión de Reservas

### 8.1 Estados

* Pending
* Confirmed
* Cancelled
* Expired

### 8.2 Expiración

Las reservas contienen un `ExpiresAt`.

Si no son confirmadas dentro del tiempo:

* Se marcan como expiradas
* Se libera la capacidad del slot

### 8.3 Confirmación

Uso de tokens de confirmación con expiración.

---

## 9. Sistema de Pagos

Integración con Stripe:

* Checkout Sessions
* Payment Intents
* Webhooks

### 9.1 Webhooks

Permiten sincronizar el estado del pago con el sistema.

**Ubicación del diagrama:**

```
/docs/images/payment-flow.png
```

---

## 10. Reglas de Precio

### 10.1 BasePriceRule

* Multiplicador por rango horario
* Aplicación mediante bitmask por día

### 10.2 PricingRule

* Asociadas a horarios específicos
* Permiten personalización avanzada

---

## 11. Concurrencia

Se implementa **optimistic concurrency** mediante:

* RowVersion en entidades críticas

Esto evita sobrescrituras en escenarios concurrentes.

---

## 12. Mensajería Asincrónica

Uso de RabbitMQ para desacoplar procesos.

### 12.1 Colas

* `emails`
* `expire-reservation`

### 12.2 Workers

* **worker.email**: envío de notificaciones
* **worker.business**: lógica de expiración

**Ubicación del diagrama:**

```
/docs/images/rabbitmq-flow.png
```

---

## 13. Background Services

Procesos internos para:

* Expiración de reservas
* Ejecución de tareas programadas

---

## 14. Políticas de Uso

* Solo usuarios activos pueden ejecutar operaciones
* Validaciones aplicadas en capa de aplicación

---

## 15. Documentación de API

El sistema expone documentación mediante Swagger:

* Visualización de endpoints
* Pruebas en tiempo real
* Contratos de entrada y salida

---

## 16. Estructura del Proyecto

---

## 17. Diagramas





## 18. Tecnologías

* .NET
* Entity Framework Core
* RabbitMQ
* Stripe
* Swagger

---


