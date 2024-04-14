moment.suppressDeprecationWarnings = true;
jQuery.extend({
    dateNow: function () {
        //console.log(new Date());
      return moment(new Date()).format('YYYY-MM-DD');
  },
  Now: function () {
    //console.log(new Date());
    return moment(new Date()).format('YYYY-MM-DD HH:mm:ss');
  },
    isDateVaild: function (value) {
       
        var regex = new RegExp("^\\d{1,2}\\/\\d{1,2}\\/\\d{4}$");
        return regex.test(value);
    },
    isDateTimeVaild: function (value) {

        var regex = new RegExp("/(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})/");
        return regex.test(value);
    },
    getUserName: function () {
        return $('#currentuser').val();
  },
    uuid : function () {
      var d = new Date().getTime();
      var guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
      });
      return guid;
  },
  postDownload: function (url, formData, onCompleted) {
    return new Promise(function (resolve, reject) {
      var filename = '';
      var xhr = new XMLHttpRequest();
      xhr.open('POST', url, true);
      xhr.responseType = 'blob';
      xhr.overrideMimeType("application/vnd.ms-excel");
      xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
          //console.log(xhr.getResponseHeader('Content-Disposition'));
          var header = xhr.getResponseHeader('Content-Disposition');
          if (header) {
            var regx = /filename[^;=\n]*=((['\"]).*?\2|[^;\n]*)/g;
            filename = regx.exec(header)[1];
            //console.log(regx.exec(header));
          }
          var blob = xhr.response;
          saveAs(blob, filename);
          if (onCompleted !== undefined)
            onCompleted(filename);

        } else if (xhr.status === 500) {
          reject({
            status: this.status,
            statusText: this.statusText
          });
        }
      };
      xhr.onerror = function () {
        reject({
          status: this.status,
          statusText: this.statusText
        });
      };
      xhr.onload = function (e) {
        resolve({
          filename: filename,
          status: this.status,
          statusText: xhr.statusText
        });
      };
      //var formData = new FormData();
      //formData.append('filterRules', filterRules);
      //formData.append('sort', 'Id');
      //formData.append('order', 'asc');
      xhr.send(formData);
    });
  }
});
 
