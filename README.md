
# 🎬 Movie App - Prueba Técnica

Aplicación de escritorio construida con ASP.NET Core Web API (backend) y WPF (frontend) que permite buscar películas usando la API pública de OMDb y guardar favoritas por usuario. Diseñada como parte de la prueba técnica.

---

## 🚀 Tecnologías Utilizadas

### Backend
- **Lenguaje:** C#
- **Framework:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core
- **Base de datos:** PostgreSQL en un servidor remoto usando Neon (plataforma gratuita para despliegue de bases de datos).
- **Autenticación:** JWT (Json Web Token). Se implementó, pero no se utilizó completamente debido a la simplicidad del proyecto.
- **Arquitectura:** Arquitectura limpia, separada en tres capas:
  - **Presentación**: Controladores y DTOs, responsables de la comunicación con el frontend y transporte de datos.
  - **Servicio**: Contiene la lógica de negocio y la coordinación de operaciones.
  - **Persistencia**: Entidades y repositorios, manejo directo con la base de datos.
- **Principios aplicados:** Programación orientada a interfaces y Principio de Inversión de Dependencias (DIP), Single Responsibility, etc.

### Frontend
- **Plataforma:** WPF (.NET 8)
- **Lenguaje:** C#
- **Interfaz gráfica:** XAML
- **Comunicación con backend:** HttpClient y deserialización JSON (peticiones básicas HTTP).
- **Manejo de sesión:** Implementación de `SessionManager` para gestionar el usuario autenticado durante la sesión, evitando pedir datos repetidos como el ID del usuario.

---

## 📸 Funcionalidades

### Backend (ASP.NET Core)
- ✅ Registro de usuarios (`POST /api/user`)
- ✅ Login con JWT (`POST /api/auth/login`)
- ✅ Buscar películas usando OMDb (`GET /api/movie/search?title=...`)
- ✅ Guardar películas favoritas (`POST /api/favoritemovie`)
- ✅ Eliminar de favoritos (`DELETE /api/favoritemovie?userId=...&movieId=...`)
- ✅ Listar favoritos de un usuario (`GET /api/favoritemovie/user/{userId}`)

### Frontend (WPF)
- ✅ Pantalla de Login y Registro
- ✅ Búsqueda de películas simulando una experiencia tipo "Single Page"
- ✅ Visualización dinámica de íconos de "Agregar/Quitar Favorito"
- ✅ Listado de películas favoritas (solo cuando el usuario está autenticado)
- ✅ Ocultamiento de botones cuando no hay sesión activa
- ✅ Interfaz funcional y clara
- ⚠️ Nota: Algunas películas no tienen imagen de póster y se muestran en blanco. No se añadió imagen por defecto para mantener la simplicidad del proyecto.

---
## 🔧 Cómo Ejecutar el Proyecto

### 🔐 Requisitos Previos

- Tener instalado [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
- Tener Visual Studio con los workloads:
  - **.NET Desktop Development**
  - **ASP.NET Core Web Development**

---

### 1. Clonar el Repositorio

```bash
git clone https://github.com/estebanhv/MovieWebApi.git
cd MovieWebApi
```

---

### 2. Configurar el Backend

1. La solución contiene dos proyectos:
   - `MovieWebApi` → backend
   - `MovieDesktopApp` → frontend

2. Ir al proyecto `MovieWebApi`.

3. Crear el archivo `appsettings.json` en la raíz del proyecto (el contenido se suministra por correo):

```json
{
  "OmdbApi": {
    "ApiKey": "TU_CLAVE_API"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=...;Database=...;Username=...;Password=..."
  },
  "Jwt": {
    "Key": "CLAVE_SUPER_SECRETA",
    "Issuer": "MovieAPI",
    "Audience": "MovieAppClient"
  }
}
```

> ⚠️ No necesitas aplicar migraciones. La base ya está creada y actualizada.

4. Ejecutar el backend desde la raíz del proyecto:

```bash
dotnet run --launch-profile "https"
```

> Accesible por defecto en `https://localhost:7037`

---

### 3. Ejecutar el Frontend (WPF)

1. Abrir la solución en Visual Studio.
2. Establecer `MovieDesktopApp` como proyecto de inicio.
3. También puedes ejecutarlo desde consola:

```bash
cd MovieDesktopApp
dotnet run
```

---
---

## 🧠 Decisiones Técnicas

- ✅ Separación por capas: Presentación, Servicio, Persistencia
- ✅ En lugar de realizar búsquedas repetidas contra la API de OMDb cada vez que un usuario consulta sus películas favoritas, se decidió almacenar directamente en la base de datos las películas que el usuario marca como favoritas. Esto evita llamadas innecesarias, mejora la velocidad de carga y permite escalabilidad futura.
- ✅ Se optó por usar el ImdbId como clave primaria o alterna para las películas almacenadas localmente, asegurando consistencia con la API externa, evitando duplicados y facilitando la lógica de favoritos.
- ✅ Se implementó lógica de validación para evitar duplicados tanto en la tabla de películas como en la de favoritos.
- ✅ Hay una tabla intermedia entre `User` y `Movie` llamada `FavoriteMovieEntity`, que permite representar de manera correcta y escalable la lista de películas favoritas por usuario.
- ✅ Existe búsqueda pública, accesible sin login, pero se restringieron todas las acciones relacionadas con favoritos a usuarios autenticados. Esto mejora la experiencia de usuario inicial y protege las operaciones sensibles.
- ✅ Manejo de sesión básico con `SessionManager` en el frontend.
- ✅ JWT implementado para autenticación segura (aunque no usado en profundidad).
- ✅ No se utilizaron variables de entorno y los puertos estan quemados pero fue por simplicidad ,para que sea mas senciloo ejecutar , las credenciales de base de datos, jwt , y api si se dan en el correo.


---

## 💡 Posibles Mejoras Futuras

- 🔄 Paginación en resultados de búsqueda
- 🔍 Filtros por género, año u otros criterios
- 🌐 Versión web con React, Angular u otro framework moderno

---

## 📄 Licencia

MIT © 2025 Esteban Henao
