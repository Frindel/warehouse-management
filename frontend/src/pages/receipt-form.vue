<script setup>

</script>

<template>
  <article class="content px-4">
    <h2>Поступление</h2>
    <div class="actions">
      <button class="btn btn-success">Сохранить</button>
      <button
          class="btn btn-danger">Удалить
      </button>
    </div>
    <br>
    <div>
      <table>
        <tbody>
        <tr>
          <td>Номер</td>
          <td><input class="form-control"></td>
        </tr>
        <tr>
          <td>Дата</td>
          <td><input class="form-control" type="date"></td>
        </tr>
        </tbody>
      </table>
      <br>
      <table class="table-bordered">
        <thead>
        <tr>
          <td>
            <button class="btn btn-success">+</button>
          </td>

          <th>Ресурс</th>

          <th>Единица измерения</th>

          <th>Количество</th>
        </tr>
        </thead>
        <tbody>
        <tr>
          <td>
            <button class="btn btn-danger">x</button>
          </td>
          <td>
            <select class="form-control">
              <option value="01986707-fd62-743f-8ba8-2d390f750c98">63</option>
            </select>
          </td>
          <td>
            <select class="form-control">
              <option value="01987b3a-2459-7331-bbe5-148750fe5547">19</option>
            </select>
          </td>
          <td>
            <input class="form-control">
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </article>
</template>

<style scoped>

</style>