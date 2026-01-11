import { NG_EVENT_PLUGINS, provideEventPlugins } from "@taiga-ui/event-plugins";
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { authInterceptor, refreshTokenInterceptor } from "./core/auth/interceptors/auth.interceptor";

export const appConfig: ApplicationConfig = {
    providers: [
        provideAnimationsAsync(),
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes, withViewTransitions()),
        provideHttpClient(
            withInterceptors([
            authInterceptor,
            refreshTokenInterceptor
            ])
        ),
        provideEventPlugins()
    ]
};
