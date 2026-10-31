# MediStock - Sistema de Gestión de Inventario Médico

**Universidad Fidélitas**  
**Carrera:** Ingeniería en Sistemas de Computación  
**Curso:** Programación Avanzada (SC-701)  
**Profesor:** Richard Arce Vargas  

---

## 1. Integrantes del Grupo

| Nombre Completo | Correo Institucional |
| :--- | :--- |
| Steven Mauricio Fonseca Herrera | sfonseca10995@ufide.ac.cr |
| Erick Alejandro Barrientos Montes | ebarrientos80448@ufide.ac.cr |
| Clark Delenny Samuels Gonzalez | csamuels00989@ufide.ac.cr |
| Edwin Camilo Pardo Garcia | epardo32120@ufide.ac.cr |

---

## 2. Repositorio del Proyecto

- **Enlace de GitHub:** [https://github.com/StevenFH/SistemaMedico.git](https://github.com/StevenFH/SistemaMedico.git)
- **Profesor invitado:** `Richard.arce.vargas@gmail.com`

---

## 3. Especificación Técnica y Arquitectura

### 3.1. Arquitectura del Proyecto (.NET Core)
El sistema está construido siguiendo los principios de **Clean Architecture** (Arquitectura Limpia), estructurado en 4 capas estrictas para garantizar la separación de responsabilidades y la escalabilidad:

- **1. Domain (`SistemaMedico.Domain`):** El núcleo del sistema. Contiene las entidades de negocio (`Categoria`, `Producto`) y las interfaces fundamentales (ej. `IGenericRepository`). No tiene dependencias de ningún otro proyecto.
- **2. Application (`SistemaMedico.Application`):** Contiene la lógica de negocio y casos de uso (`CategoriaService`, `ProductoService`), los DTOs para la transferencia segura de datos y los perfiles de mapeo (AutoMapper). Depende únicamente de `Domain`.
- **3. Infrastructure (`SistemaMedico.Infrastructure`):** Maneja la persistencia y el acceso a datos. Implementa el DbContext (Entity Framework Core) y el Repositorio Genérico (`GenericRepository`). Depende de `Domain` y `Application`.
- **4. Presentation (`SistemaMedico` - ASP.NET Core MVC):** La capa visual (UI) interactiva con el usuario. Consume los servicios, gestiona los Controladores, las Vistas Razor con Bootstrap y el Middleware global de excepciones.

### 3.2. Paquetes NuGet y Librerías Utilizadas
- `Microsoft.EntityFrameworkCore.SqlServer`: Proveedor de base de datos para SQL Server.
- `Microsoft.EntityFrameworkCore.Tools`: Herramientas para la ejecución de comandos en consola (Migraciones).
- `Microsoft.EntityFrameworkCore.Design`: Requerido para el diseño en tiempo de ejecución de la base de datos.
- `AutoMapper`: Herramienta para mapear de manera automática y eficiente las Entidades del Dominio hacia los DTOs de la vista (y viceversa).

### 3.3. Principios SOLID Implementados
- **Single Responsibility Principle (SRP):** Cada clase tiene una única responsabilidad. Los controladores solo manejan solicitudes HTTP, los servicios manejan las reglas de negocio y los repositorios manejan el acceso a la base de datos.
- **Open/Closed Principle (OCP):** El uso del Repositorio Genérico permite extender el acceso a datos para nuevas entidades sin tener que modificar la clase base `GenericRepository`.
- **Liskov Substitution Principle (LSP):** La inyección a través de interfaces (`ICategoriaService`, `IGenericRepository`) garantiza que cualquier implementación futura pueda sustituir a la actual sin romper el sistema.
- **Interface Segregation Principle (ISP):** Interfaces enfocadas y específicas (`ICategoriaService` y `IProductoService`) en lugar de una interfaz gigantesca para todos los servicios del sistema.
- **Dependency Inversion Principle (DIP):** Las capas superiores (Presentación/Aplicación) no dependen de las implementaciones concretas (Infraestructura), sino de las abstracciones (Interfaces) inyectadas en el contenedor de `Program.cs`.

### 3.4. Patrones de Diseño Utilizados
- **Repository Pattern (Genérico):** Centraliza el acceso a la base de datos, reduciendo la duplicación de código mediante el uso de Generics (`<T>`).
- **Dependency Injection (DI):** Registro e inyección de servicios, repositorios y AutoMapper en el ciclo de vida de la aplicación.
- **DTO (Data Transfer Object):** Separación de los modelos de base de datos de los datos expuestos en la interfaz de usuario.
- **Middleware Pattern:** Implementación de un `MiddlewareGlobalExceptionHandler` para capturar errores transversalmente y devolver respuestas estructuradas al usuario sin romper el flujo.

---

## 4. Módulos y Funcionalidades del Sistema

1. **Gestión de Categorías (CRUD):**
   - Mantenimiento completo de familias de insumos médicos.
   - **Regla de Negocio Protegida:** El sistema valida (mediante el servicio y el Middleware) que no se pueda eliminar una categoría si existen productos vinculados a ella, informando al usuario correctamente.
2. **Gestión de Productos Médicos (CRUD):**
   - Control de inventario (Nombre, Precio, Cantidad, Descripción).
   - Asignación dinámica de categorías mediante ComboBox.
3. **Panel de Búsqueda y Filtrado:**
   - La tabla principal de productos incluye filtros funcionales por nombre, categoría y precio máximo utilizando consultas LINQ eficientes con `Includes` para las tablas relacionales.
4. **Data Seeding:**
   - Inserción automática de catálogos iniciales (Categorías base y productos de ejemplo con precios en moneda local) al crear la base de datos.

---

## 5. Instrucciones de Ejecución

1. **Requisitos previos:**
   - .NET SDK (v8.0 o superior recomendado).
   - Visual Studio 2022 o superior.
   - Instancia local de SQL Server (LocalDB configurada en la cadena de conexión).

2. **Restaurar el proyecto:**
   Al abrir la solución (`SistemaMedico.sln`) en Visual Studio, las dependencias de NuGet se restaurarán automáticamente.

3. **Aplicar migraciones y crear la Base de Datos:**
   - Abrir la **Consola del Administrador de paquetes** en Visual Studio.
   - Seleccionar **SistemaMedico.Infrastructure** como proyecto predeterminado.
   - Ejecutar el comando para crear la estructura e insertar la data inicial (Seed Data):
     ```bash
     Update-Database -StartupProject SistemaMedico
     ```

4. **Ejecutar:**
   - Asegurarse de que el proyecto principal (`SistemaMedico` / Presentación) esté seleccionado como el proyecto de inicio.
   - Presionar `F5` o el botón "Iniciar" (https).
