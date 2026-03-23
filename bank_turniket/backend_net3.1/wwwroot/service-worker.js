self.addEventListener('install', event => {
  console.log('Service Worker: installed');
  self.skipWaiting(); // tez aktivlashish uchun
});

self.addEventListener('activate', event => {
  console.log('Service Worker: activated');
  return self.clients.claim();
});