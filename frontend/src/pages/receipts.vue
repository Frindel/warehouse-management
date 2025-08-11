<script setup>

import TheReceiptsFilter from "@/components/the-receipts-filter.vue";
</script>

<template>
  <article class="content px-4">

    <h2>Поступления</h2>
    <the-receipts-filter></the-receipts-filter>
    <br>
    <div class="row">
      <div class="col actions">
        <button class="btn btn-primary">Применить</button>
        <a href="#" class="btn btn-success">Добавить</a>
      </div>
    </div>
    <br>
    <table class="table-bordered">
      <thead>
      <tr>
        <th>Номер</th>
        <th>Дата</th>
        <th>Ресурс</th>
        <th>Единица измерения</th>
        <th>Количество</th>
      </tr>
      </thead>
      <tbody>
      <tr>
        <td rowspan="4">rSP--000203</td>
        <td rowspan="4">05.08.2025</td>
        <td>каа</td>
        <td>рррр</td>
        <td>1</td>
      </tr>
      <tr>
        <td>Состояние</td>
        <td>678</td>
        <td>5</td>
      </tr>
      <tr>
        <td>Состояние</td>
        <td>шт</td>
        <td>1</td>
      </tr>
      <tr>
        <td>каа</td>
        <td>Кстати</td>
        <td>112</td>
      </tr>
      </tbody>
    </table>
  </article>
</template>