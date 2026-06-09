# Instrucciones de Comportamiento para la IA

1. **Revisión Obligatoria:** Antes de generar cualquier archivo de código, lee `docs/01-PRODUCT-SPEC.md` y `docs/02-ARCHITECTURE.md`.
2. **Stack Tecnológico Estricto:** 
   - .NET 8.0 / .NET MAUI (C# 12).
   - MVVM usando `CommunityToolkit.Mvvm` (Usa obligatoriamente Source Generators: `[ObservableProperty]` y `[RelayCommand]`).
   - Base de datos: `sqlite-net-pcl` (Usa la API asíncrona `SQLiteAsyncConnection`).
3. **Restricciones de Código:**
   - Prohibido instalar NuGets adicionales sin preguntar primero.
   - Prohibido escribir lógica de negocio en los archivos `.xaml.cs` (code-behind). Todo va en las ViewModels.
   - Las propiedades observables deben ser privadas y en camelCase (ej: `private string _name;`) para que el Source Generator cree la propiedad pública en PascalCase (`Name`).
4. **Manejo de Errores:** Toda petición HTTP en `ApiService` y consulta en `DatabaseService` debe estar envuelta en bloques `try-catch` capturando excepciones específicas e informando al ViewModel.