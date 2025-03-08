    # VueChain

📖 Índice

- 📌 Problemática
- 🎯 Objetivo
- 👥 Integrantes
- 🛠 Tecnologías y Librerías
- 🚀 Cómo Ejecutar el Proyecto
- 🤓 Modelo de Trabajo
- 📢 Contribución
- 📧 Contacto

📌 Problemática

El mercado de criptomonedas es altamente volátil y muchos usuarios no cuentan con herramientas accesibles para monitorear precios en tiempo real, analizar tendencias o simular operaciones sin riesgos. Además, la falta de información clara y herramientas de análisis limita la toma de decisiones informadas, lo que puede llevar a pérdidas económicas.

🎯 Objetivo

Desarrollar una aplicación web que permita a los usuarios monitorear precios de criptomonedas en tiempo real, analizar tendencias y simular operaciones sin riesgos, proporcionando herramientas de análisis claras y accesibles para mejorar la toma de decisiones.

👥 Integrantes

| Nombre | Rol | GitHub | Correo Institucional     |
| --- | --- | --- |--------------------------|
| Arbey | Líder del Proyecto | [GitHub](https://github.com/Joestarb) | 22393167@utcancun.edu.mx |
| Angel | Desarrollador Backend | [GitHub](https://github.com/Joestarb) | correo@institucion.edu   |
| Ross | Desarrollador Backend | [GitHub](https://github.com/Joestarb) | correo@institucion.edu   |
| Soto | Desarrollador Frontend | [GitHub](https://github.com/Joestarb) | correo@institucion.edu   |
| Gopar | Colaborador Frontend | [GitHub](https://github.com/Joestarb) | correo@institucion.edu   |

🛠 Tecnologías y Librerías

📌 Backend

- .NET 8 con ASP.NET Core - API RESTful
- Entity Framework Core - ORM para persistencia de datos
- JWT (JSON Web Tokens) - Autenticación y autorización de usuarios
- PostgreSQL - Base de datos relacional (Producción)
- SQLite - Base de datos embebida (Desarrollo)

🎨 Frontend

- Vue.js - Framework para el desarrollo de la interfaz de usuario
- Vue Router - Manejo de navegación entre vistas
- Axios - Comunicación con el backend
- Prime Vue - Estilización rápida y responsiva
- Vee Validate - Validación de formularios
- Vue Query - Manejo de peticiones y caché de datos
- Vue Toastification - Notificaciones de alerta

📊 Visualización de Datos

- Chart.js o ECharts - Representación de gráficos de precios y tendencias

📌 Gestión del Código

- Git con Git Flow - Control de versiones y manejo de ramas

🚀 Cómo Ejecutar el Proyecto

### Backend (.NET 8 con ASP.NET Core)

Clonar el repositorio:

```bash
git clone https://github.com/Joestarb/vueChain-back.git
cd vueChain-back
```


Instalar dependencias:

```bash
dotnet restore
```
Ejecutar el prouecto en modo desarollo

```bash
dotnet run
```
Forontend (Vue.js)
clonar el repositorio

```bash 
git clone https://github.com/Joestarb/VueChain.git
cd VueChain
```
instalar dependencias

```bash
npm install
```
correr el proyecto en modo desarrollo

```bash
npm run dev
```
Abrir en el navegador http://localhost:5173.  

🤓 Modelo de Trabajo 

Para mantener una estructura organizada, utilizamos Git Flow como metodología de trabajo. Esto nos permite manejar mejor el desarrollo y la integración de nuevas funcionalidades.  

🔹 Ramas principales 

main: Contiene el código en producción, estable y listo para su implementación.
develop: Contiene el código en desarrollo, donde se integran nuevas características antes de fusionarlas en main.

🔹 Flujo de trabajo  

Cada integrante crea su rama de trabajo a partir de develop.
Una vez terminada la funcionalidad, se realiza un Pull Request (PR) hacia develop.
El PR debe ser revisado y aprobado por al menos un integrante antes de fusionarlo.
Periódicamente, develop se fusiona con main para lanzar una nueva versión estable.

📌 Reglas del equipo:  
Cada PR debe incluir una descripción clara del cambio realizado.
No se permite fusionar código directamente en main o develop.
Se deben realizar pruebas antes de enviar cambios para revisión.

📢 Contribución  La colaboración está limitada al equipo de desarrollo, pero si encuentras un error o tienes una idea para mejorar el proyecto, ¡puedes abrir un issue en el repositorio!  📧 Contacto  Si tienes dudas o sugerencias, puedes comunicarte con cualquiera de los integrantes del equipo.

© 2024 VueChain. Todos los derechos reservados.