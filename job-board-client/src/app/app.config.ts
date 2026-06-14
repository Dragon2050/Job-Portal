import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors, withFetch } from '@angular/common/http';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    // 1. Enable standard browser global error boundaries
    provideBrowserGlobalErrorListeners(),

    // 2. Enable Angular routing system
    provideRouter(routes),

    // 3. Register our HTTP engine with fetch support and our JWT Auth Interceptor
    provideHttpClient(
      withFetch(),
      withInterceptors([authInterceptor])
    ),

    // 4. SSR (Server-Side Rendering) Hydration support
    provideClientHydration(withEventReplay())
  ]
};
