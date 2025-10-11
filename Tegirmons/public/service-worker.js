self.addEventListener("install", event => {
  console.log("Service Worker installed");
});

self.addEventListener("activate", event => {
  console.log("Service Worker activated");
});

self.addEventListener("fetch", event => {
  // Caching logic yoki offline rejimni keyin qo‘shish mumkin
});