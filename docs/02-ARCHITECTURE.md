# Especificación Arquitectónica: .NET MAUI Enterprise

## 1. Patrón de Arquitectura: MVVM Estricto
- **Models:** Entidades de datos puras (DTOs y tablas SQLite). Sin lógica de negocio.
- **ViewModels:** Lógica de presentación. Deben heredar de `ObservableObject` utilizando el `CommunityToolkit.Mvvm` (Source Generators para `[ObservableProperty]` y `[RelayCommand]`).
- **Views:** Archivos XAML. Prohibido escribir lógica de negocio en el *code-behind* (`.xaml.cs`). Solo se permite inicialización de componentes o interacciones visuales puras.

## 2. Estructura de Directorios del Proyecto
```text
/src
 ├── Models/          # Product.cs, Order.cs, OrderItem.cs
 ├── ViewModels/      # CatalogViewModel.cs, CartViewModel.cs, BaseViewModel.cs
 ├── Views/           # CatalogPage.xaml, CartPage.xaml
 ├── Services/        # IApiService, ISqliteService, IConnectivityService
 ├── Helpers/         # Converters, Selectors
 ├── AppShell.xaml    # Enrutamiento y navegación centralizada
 └── MauiProgram.cs   # Configuración de Contenedor DI (Dependency Injection)