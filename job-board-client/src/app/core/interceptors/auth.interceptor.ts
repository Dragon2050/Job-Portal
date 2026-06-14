import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // 1. Only read localStorage if the code is running in the browser
  const token = typeof window !== 'undefined' ? localStorage.getItem('token') : null;

  // 2. If a token exists, clone and append authorization header
  if (token) {
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(clonedRequest);
  }

  return next(req);
};
