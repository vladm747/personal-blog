/** @type {import('tailwindcss').Config} */
import defaultTheme from 'tailwindcss/defaultTheme';
export default {
    darkMode: 'class',
  content: [
      "./src/index.html",
      "./src/**/*.jsx",
  ],
  theme: {
    extend: {
        fontFamily: {
            sans: ['Inter var', ...defaultTheme.fontFamily.sans],
        },
    },
  },
  plugins: [],
}

