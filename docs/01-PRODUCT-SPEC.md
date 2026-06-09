# Especificación de Producto: Multiplatform Field Sales Force

## 1. Propósito del Sistema
Aplicación multiplataforma (.NET MAUI) de preventa y toma de pedidos para agentes de campo (móvil) y supervisores (escritorio), conectada a una API REST y con capacidad de operación 100% offline.

## 2. Características Clave y Reglas de Negocio

### A. Catálogo Interactivo y Carrito
- **Exploración:** El usuario puede listar productos, buscar por texto y filtrar por categoría.
- **Carrito de Compras:** Permite añadir productos, modificar cantidades, calcular subtotales y totalizar el pedido.
- **Estados del Pedido:** Un pedido puede estar en estado `Draft` (en carrito) o `Submitted` (finalizado por el agente).

### B. Resiliencia y Operatividad Offline (Offline-First)
- **Detección de Red:** El sistema debe validar el estado de la conexión antes de enviar peticiones HTTP.
- **Modo Offline Automático:** Si no hay red, las solicitudes HTTP se suspenden de inmediato. Los pedidos se guardan localmente en la base de datos SQLite con la bandera `IsPendingSync = true`.
- **Sincronización Asíncrona:** Al recuperar la conectividad, el sistema dispara un proceso en segundo plano que envía los pedidos pendientes a la API central en orden cronológico. Una vez confirmado por la API, `IsPendingSync` pasa a `false`.

### C. Experiencia de Usuario Adaptativa (Responsive)
- **Diseño Móvil (Android/iOS):** Vista compacta, lista vertical de productos tipo tarjeta, botones grandes y optimizados para uso táctil en movilidad.
- **Diseño Escritorio (Windows/macOS):** Dashboard denso multi-columna. Panel izquierdo para catálogo/filtros y panel derecho fijo para visualización del carrito actual.