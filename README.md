# SimpleBlazorApp
CRUD con Blazor

Este es un proyecto CRUD desarrollado en Blazor con .NET 7 que te permite realizar operaciones básicas (Crear, Leer, Actualizar y Eliminar) en una lista de productos. Utiliza la inyección de dependencias para gestionar las operaciones de datos y se comunica con un API para interactuar con el backend.

Requisitos Previos
.NET Core SDK instalado en tu máquina.
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Sqlite

Instalación
Clona este repositorio en tu máquina local:

bash
Copy code
git clone https://github.com/tu-usuario/tu-proyecto.git
Navega al directorio del proyecto:

bash
Copy code
cd tu-proyecto
Ejecuta la aplicación:

bash
Copy code
dotnet run
Abre tu navegador y visita https://localhost:5001 para interactuar con la aplicación.

Estructura del Proyecto
SimpleBlazorApp: Contiene el proyecto Blazor.
Pages: Contiene las páginas y componentes Blazor.
Services: Contiene las clases para la inyección de dependencias y la lógica de negocio.
SimpleApi: Contiene el proyecto de API backend.
Controllers: Contiene los controladores API para gestionar las operaciones CRUD.
Models: Contiene las clases de modelos utilizadas por el backend.
Características
Interfaz de usuario intuitiva para realizar operaciones CRUD en productos.
Uso de la inyección de dependencias para separar la lógica de negocio de la interfaz de usuario.
Comunicación con una API backend para almacenar y recuperar datos.
Componentes reutilizables para mostrar la lista de productos y los formularios de edición/agregación.
Estructura modular del proyecto para facilitar la escalabilidad y el mantenimiento.
Contribución
Si deseas contribuir a este proyecto, por favor sigue estos pasos:

Haz un fork de este repositorio.
Crea una rama con la nueva característica o solución de problema: git checkout -b nueva-caracteristica
Realiza tus cambios y realiza un commit: git commit -m "Añadir nueva característica"
Envía tus cambios al repositorio remoto: git push origin nueva-caracteristica
Crea un pull request en este repositorio.
Licencia
Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.




