import './app.css';
import 'prismjs/themes/prism.css';
import Prism from 'prismjs';

window.Prism = Prism;
window.highlightCode = () => Prism.highlightAll();
window.blazorCulture = {
    get: () => {
        const raw = window.localStorage['BlazorCulture'] ?? navigator.language;
        const locale = new Intl.Locale(raw).maximize();
        return `${locale.language}-${locale.script}`;
    },
    set: (value) => window.localStorage['BlazorCulture'] = value
};
