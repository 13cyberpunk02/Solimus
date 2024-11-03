/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {    
    extend: {
      fontFamily: {
        sans: ["Russo One", 'sans-serif']
      }
    },
  },
  plugins: [],
}

