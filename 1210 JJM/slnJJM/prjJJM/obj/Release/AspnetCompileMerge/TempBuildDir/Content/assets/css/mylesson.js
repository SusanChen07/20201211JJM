// $(document).ready(function(){
//     $('.show-details').click(function(){
//       console.log($(this).find('.fa.fa-angle-down'));
//       $(this).find('i')
//              .toggleClass('fa-angle-down fa-angle-up');
      
//       $(this).siblings('.details')
//              .toggleClass('open')
//              .slideToggle('milliseconds');
//      });
//   });

//$('.collapse').not(':first').collapse(); // Collapse all but the first row on the page.

// This section makes the search work.
(function() {
  var searchTerm, panelContainerId;
  $('#accordion_search_bar').on('change keyup', function() {
    searchTerm = $(this).val();
    $('#accordion > .panel').each(function() {
      panelContainerId = '#' + $(this).attr('id');

      // Makes search to be case insesitive 
      $.extend($.expr[':'], {
        'contains': function(elem, i, match, array) {
          return (elem.textContent || elem.innerText || '').toLowerCase()
            .indexOf((match[3] || "").toLowerCase()) >= 0;
        }
      });

      // END Makes search to be case insesitive

      // Show and Hide Triggers
      $(panelContainerId + ':not(:contains(' + searchTerm + '))').hide(); //Hide the rows that done contain the search query.
      $(panelContainerId + ':contains(' + searchTerm + ')').show(); //Show the rows that do!

    });
  });
}());
// End Show and Hide Triggers

// END This section makes the search work.

//星星

$(function(){
  // 星星選擇評價事件
  $(".score_star >i").click(function(event) {
      // 點擊當前
      var _index = $(this).index();
      // 所有的星星
      var i = $(this).parent().find("i");
          i.removeClass("on");
      // 點擊第i個，第一個到i個添加類名on
      switch(_index){
          case 0:
              i.slice(0,1).addClass("on");
              $(this).siblings('input').val("1");
          break;
          case 1:
              i.slice(0,2).addClass("on");
              $(this).siblings('input').val("2");
          break;
          case 2:
              i.slice(0,3).addClass("on");
              $(this).siblings('input').val("3");
          break;
          case 3:                        
              i.slice(0,4).addClass("on");
              $(this).siblings('input').val("4");
          break;
          case 4:
              i.slice(0,5).addClass("on");
              $(this).siblings('input').val("5");
          break;
          default:
              alert("哇!評分壞掉了，麻煩聯絡我們以修理星星");
          break;
      }
  });
});

//送出
// (function() {
//   var searchTerm, panelContainerId;
//   $('#accordion_search_bar').on('change keyup', function() {
//     searchTerm = $(this).val();
//     $('#accordion > .panel').each(function() {
//       panelContainerId = '#' + $(this).attr('id');

//       // Makes search to be case insesitive 
//       $.extend($.expr[':'], {
//         'contains': function(elem, i, match, array) {
//           return (elem.textContent || elem.innerText || '').toLowerCase()
//             .indexOf((match[3] || "").toLowerCase()) >= 0;
//         }
//       });

//       // END Makes search to be case insesitive

//       // Show and Hide Triggers
//       $(panelContainerId + ':not(:contains(' + searchTerm + '))').hide(); //Hide the rows that done contain the search query.
//       $(panelContainerId + ':contains(' + searchTerm + ')').show(); //Show the rows that do!

//     });
//   });
// }());