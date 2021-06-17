<template>
  <div class="footer-baner">
    <ul class="footer-baner-item">
      <li>
        <span>© Copyright 2021</span>
        <router-link to="/"> Dwells AB</router-link>
        <span> All Rights Reserved.</span>
      </li>
    </ul>
    <ul class="footer-baner-item">
      <li>
        <router-link to="/privacy-and-policy">
          <span>Privacy Policy</span>
        </router-link>
      </li>
      <li>
        <router-link to="/terms-and-conditions">
          <span>Terms & Conditions</span>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<style scoped>
.footer-baner {
  border-top: 1px solid #bdbdbd;
  padding: 18px 0;
  display: flex;
  justify-content: space-between;
  padding-inline-end: 40px;
}

.footer-baner-item {
  list-style: none;
  display: flex;
}
.footer-baner-item li {
  margin-right: 5px;
}

a {
  color: inherit;
  text-decoration: none;
}
</style>