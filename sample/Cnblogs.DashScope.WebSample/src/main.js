import './app.css'
import 'prismjs/themes/prism.css'
import Prism from 'prismjs'

window.Prism = Prism;
window.highlightCode = () => Prism.highlightAll();
window.blazorCulture = {
    get: () => window.localStorage['BlazorCulture'] ?? navigator.language,
    set: (value) => window.localStorage['BlazorCulture'] = value
};
