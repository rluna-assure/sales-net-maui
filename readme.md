# Multiplatform Field Sales Force & Commercial Catalog System

Solución empresarial multiplataforma desarrollada en **.NET MAUI** bajo el patrón **MVVM**. El sistema permite a los agentes comerciales gestionar el catálogo de productos y registrar pedidos en movilidad, garantizando una operatividad continua del 100% mediante un enfoque *Offline-First* con **SQLite**.

---

## Stack Tecnológico

- **Framework:** .NET 8.0 (.NET MAUI)
- **Patrón de Arquitectura:** MVVM Estricto (`CommunityToolkit.Mvvm`)
- **Base de Datos Local:** SQLite (`sqlite-net-pcl`)
- **Navegación:** Centralizada vía `AppShell`
- **Contrato de API:** OpenAPI 3.0 (definido en `/docs`)

---

## Estructura de Documentación (Contexto IA)

Si vas a desarrollar o modificar este proyecto utilizando un Asistente de IA (Claude, Cursor, OpenAI), la fuente de verdad del sistema se encuentra en la carpeta `/docs`:

1. **`docs/00-AI-RULES.md`**: Reglas de oro y restricciones de código para la IA.
2. **`docs/01-PRODUCT-SPEC.md`**: Reglas de negocio (lógica del carrito y flujo de sincronización offline).
3. **`docs/02-ARCHITECTURE.md`**: Estructura del proyecto, inyección de dependencias y guías de UI adaptativa.
4. **`docs/03-API-SPEC.yaml`**: Contrato técnico de endpoints y modelos de datos.

---

## Requisitos Previos y Configuración

1. **IDE:** Visual Studio 2022 o VS Code (con las extensiones de .NET MAUI instaladas).
2. **SDK:** .NET 8 SDK o superior.

```bash
   dotnet workload install maui


run
windows
   dotnet run --project FieldSalesForce.csproj --framework net9.0-windows10.0.19041.0
android
   dotnet run --project FieldSalesForce.csproj --framework net9.0-android

