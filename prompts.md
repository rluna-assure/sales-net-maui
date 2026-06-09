El Prompt Inicial Maestro

    Actúa como un Arquitecto de Software Senior experto en .NET MAUI (C# 12, .NET 8) y Enterprise Architecture.

    He adjuntado la documentación de mi proyecto en la carpeta /docs junto con el README.md. Tu objetivo en este chat es guiarme y generar el código para construir este sistema paso a paso, utilizando exclusivamente un enfoque basado en Prompts (Spec-Driven Development).

    Antes de escribir cualquier línea de código o crear archivos, necesito que hagas lo siguiente:

        Confírmame que has leído y entendido las restricciones de código y formato de docs/00-AI-RULES.md (especialmente el uso de Source Generators para MVVM).

        Explícame brevemente, con tus propias palabras, cómo vas a estructurar el flujo de sincronización asíncrona e interoperabilidad Offline-First según docs/01-PRODUCT-SPEC.md.

        Propón el orden cronológico exacto de los siguientes 3 prompts que deberíamos ejecutar para empezar a construir la base del proyecto desde cero, asegurando que vayamos de la capa más profunda (datos) a la más superficial (UI).

    No generes archivos de código todavía. Espero tu análisis y propuesta de pasos.



----Subtasks----
    Setup .NET MAUI project and configure target platforms (Android, iOS, Windows, macOS).
    Structure project directories according to the MVVM pattern (Models, Views, ViewModels, Services).
    Install essential NuGet packages (CommunityToolkit.Mvvm, sqlite-net-pcl, System.Text.Json).
    Register Services, ViewModels, and Views in the Dependency Injection container (MauiProgram.cs).
    Define base navigation layout in AppShell.xaml using adaptive structures (Flyout for Desktop / Tabs for Mobile).
    Create data entities and models (Product, Order, Client, CartItem).
    Implement LocalDatabaseService using SQLite for local CRUD operations.
    Implement ApiService to consume the central REST API endpoints.
    Implement ConnectivityService to detect network status changes automatically.
    Develop SyncService to queue offline orders and push them asynchronously when connection is restored.
    Implement CatalogViewModel to handle product listing, category filtering, and search logic.
    Implement ProductDetailViewModel to handle product specifications and adding items to the cart.
    Implement CartViewModel to manage item quantities, calculate totals, and process order checkout.
    Implement OrderHistoryViewModel to list past orders and display their synchronization status.
    Design CatalogPage.xaml using OnIdiom and Grid to show multi-column dashboard on Desktop and a list on Mobile.
    Design ProductDetailPage.xaml with adaptive layouts (FlexLayout or responsive Grid).
    Design CartPage.xaml optimized for both touch interfaces and desktop screens.
    Test offline operability by disabling network, creating orders, and verifying SQLite storage.
    Test reconnection logic to verify automatic background synchronization with the REST API.
