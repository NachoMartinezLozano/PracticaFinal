module.exports = {
  daisyui: {
    themes: [
      {
        light: {
          ...require('daisyui/src/theming/themes')['light'],
          'base-100': '#FFFFFF', // Fondo blanco
          'base-content': '#000000', // Texto negro
          primary: '#1E40AF', // Azul oscuro para botones/enlaces
          'primary-content': '#FFFFFF', // Texto blanco en elementos primarios
        },
        dark: {
          ...require('daisyui/src/theming/themes')['dark'],
          'base-100': '#000000', // Fondo negro
          'base-content': '#FFFFFF', // Texto blanco
          primary: '#3B82F6', // Azul claro para botones/enlaces
          'primary-content': '#FFFFFF', // Texto blanco en elementos primarios
        },
      },
    ],
  },
}