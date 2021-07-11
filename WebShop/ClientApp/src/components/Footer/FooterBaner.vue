<template>
  <div class="p-grid p-col-12">
    <div class="p-grid p-col-12 p-justify-center">
      <div class="block-inline">
        <span>© 2021</span>
        <router-link to="/"> Dwells AB &nbsp;</router-link>
      </div>
      <div class="block-inline">Box 15, 56121 Huskvarna, Sweden &nbsp;</div>
      <div class="block-inline">Organization Number 559254-9587</div>
    </div>
    <div class="p-grid p-col-12 p-justify-center">
      <div class="hover-reverse">
        <router-link to="/privacy-and-policy" target="_blank">
          <small>
            Privacy Policy
            <i class="pi pi-external-link hover-reverse font-size-1-em"></i
            >&nbsp;
          </small>
        </router-link>
      </div>
      <div class="hover-reverse">
        <router-link to="/terms-and-conditions" target="_blank">
          <small>
            Terms & Conditions
            <i class="pi pi-external-link font-size-1-em"></i>&nbsp;
          </small>
        </router-link>
      </div>
    </div>
  </div>
</template>

<style scoped>
.footer-baner {
  border-top: 1px solid #bdbdbd;
  /* padding: 18px 0;
  display: flex;
  justify-content: space-between;
  padding-inline-end: 40px; */
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