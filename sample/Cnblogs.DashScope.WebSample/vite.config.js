import tailwindcss from '@tailwindcss/vite';
import { defineConfig } from 'vite';

export default defineConfig({
    plugins: [tailwindcss()],
    server: {
        watch: {
            ignored: ['wwwroot/**/*.min.js', 'wwwroot/**/*.min.css']
        }
    },
    build: {
        outDir: 'wwwroot',
        emptyOutDir: false,
        copyPublicDir: false,
        rollupOptions: {
            input: {
                main: 'src/main.js'
            },
            output: {
                entryFileNames: 'js/[name].min.js',
                assetFileNames: 'css/[name].min[extname]'
            }
        }
    }
});
