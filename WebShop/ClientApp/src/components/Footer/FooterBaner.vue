<template>
  <div class="footer-baner">
    <ul class="footer-baner-item">
      <li>
        <span>© 2021</span>
        <router-link to="/"> Dwells AB</router-link>
        <span>, Box 15, 56121 Huskvarna, Sweden</span>
      </li>
      <li>&nbsp;&nbsp;&nbsp;Org-nr 5592549587</li>
      <li>&nbsp;&nbsp;&nbsp;VAT-nr SE559254958701</li>
    </ul>
    <ul class="footer-baner-item inline">
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
  flex-direction: column;
  text-align: left;
}
.footer-baner-item li {
  margin-right: 5px;
}

.footer-baner-item.inline {
  display: flex;
  flex-direction: row;
}

a {
  color: inherit;
  text-decoration: none;
}
</style>