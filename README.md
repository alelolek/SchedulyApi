# Scheduly - API REST para Gestión de Eventos

![Arquitectura](https://github.com/alelolek/Scheduly/blob/main/DiagramaDeArquitectura.png)

## Descripción del programa

Scheduly es una poderosa API REST diseñada y desarrollada para facilitar la comunicación entre sistemas, permitiendo la gestión eficiente de programaciones. Esta solución basada en Web API .NET 6 Core se enfoca en proporcionar una interfaz segura y escalable para la administración de programaciones en diferentes aplicaciones y plataformas. Con Scheduly, los usuarios pueden crear, categorizar y establecer recordatorios para las programaciones, lo que les permite gestionar sus eventos de manera efectiva.

## Arquitectura del proyecto

 - **API (Controladores)**: La capa de API contiene los controladores que actúan como punto de entrada para las solicitudes HTTP. Los controladores se encargan de recibir las solicitudes, procesar los datos y devolver las respuestas apropiadas.
- **Capa de Aplicación**: Esta capa contiene la lógica de la aplicación, que se encarga de coordinar las interacciones entre los diferentes componentes de la API y procesar las solicitudes entrantes.
- **Capa de Acceso a Datos**: La capa de Interceptores incluye los DTO (Objetos de Transferencia de Datos) y las validaciones necesarias para garantizar la integridad de los datos y facilitar la comunicación entre diferentes partes del sistema.
- **Capa Persistencia**: En esta capa se encuentra el DbContext, las migraciones, entidades y mapeos necesarios para la persistencia de los datos en la base de datos. Scheduly utiliza un enfoque "Code First" para la generación y administración de la estructura de la base de datos.


## Características principales

- **Autenticación y Autorización**: Scheduly incorpora un sólido sistema de autenticación y autorización basado en JWT (JSON Web Tokens) y el uso de Identity. Esto garantiza que solo los usuarios autenticados y autorizados puedan acceder a las funcionalidades protegidas de la API, proporcionando un nivel adicional de seguridad.
- **Validaciones**: Scheduly implementa validaciones para asegurar la integridad de los datos y prevenir errores en la API.
- **Diseño API RESTful**: Scheduly sigue los principios de diseño de una API RESTful, proporcionando una interfaz intuitiva y fácil de usar para los clientes.
- **Arquitectura de Capas**: Scheduly sigue una arquitectura en capas, lo que permite una mejor organización y separación de responsabilidades, facilitando el mantenimiento y la escalabilidad del sistema.
- **AutoMapper**: Scheduly utiliza AutoMapper para mapear automáticamente los objetos DTO y entidades, simplificando la conversión de datos.


## Licencia

Este proyecto se distribuye bajo la licencia MIT. Para más información, consulta el archivo [LICENSE](LICENSE.txt).

¡Esperamos que Scheduly sea una herramienta valiosa para gestionar tus programaciones de manera efectiva y facilitar tu vida diaria!