
<link href="<?= $BASE_URL ?>css/opportunities.css" rel="stylesheet" type="text/css">

<br>
<button type="button" class="btn btn-primary buttonCreate" data-toggle="modal" data-target="#exampleModalLong">
  Create
</button>
<br>



<?php
function printRest($arr, $type) {
  foreach ($arr as $key => $value) {
    if(strcmp($value['opportunity_type'], $type)==0 && strcmp($value['opportunity_state'],"Open")==0){
      echo '<div class="panel panel-default" data-toggle="modal" data-target="#moreinfo">
      <div class="opor_id" hidden>' . $value['opportunity_id'] .'</div>
      <div class="opor_id" hidden>' . $value['customer_id'] .'</div>
      <div class="opor_id" hidden>' . json_encode($value['associated_activities']) .'</div>
      <div class="opor_id" hidden>' . json_encode($value['products']) .'</div>
      <div class="panel-heading">' . $value['customer_name'] . '</div>
      <div class="panel-body">
      <div class="opor-card-content opor-card-name"> Profitability: ' . calcTotalProfit($value['products']) .'</div>
      </div>
      </div>';
    }
  }
}
?>

<div class="panel panel-default mypanel">
  <div class="panel-heading">Qualification</div>
  <div class="panel-body">
    <div id="sortable1" class="connectedSortable">
      <?php printRest($resp, "Qualification");?>
    </div>
  </div>
</div>

<div class="panel panel-default mypanel">
  <div class="panel-heading">Needs analysis</div>
  <div class="panel-body">
    <div id="sortable2" class="connectedSortable">
      <?php printRest($resp, "Needs analysis");?>
    </div>
  </div>
</div>

<div class="panel panel-default mypanel">
  <div class="panel-heading">Proposal</div>
  <div class="panel-body">
    <div id="sortable3" class="connectedSortable">
      <?php printRest($resp, "Proposal");?>
    </div>
  </div>
</div>

<div class="panel panel-default mypanel">
  <div class="panel-heading">Negotiations</div>
  <div class="panel-body">
    <div id="sortable4" class="connectedSortable">
      <?php printRest($resp, "Negotiations");?>
    </div>
  </div>
</div>

<div class="panel panel-default mypanel">
  <div class="panel-heading">Ready to close</div>
  <div class="panel-body">
    <div id="sortable5" class="connectedSortable">
      <?php printRest($resp, "Ready to close");?>
    </div>
  </div>
</div>


<!-- Modal -->
<div class="modal fade" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title" id="exampleModalLongTitle">Create a new oportunity</h1>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form action="<?= $BASE_URL?>actions/opportunity/create_opportunity.php" method="post">
        <div class="modal-body">
          <h4>Customer id:</h4>
          <select name="customer_id">
            <?php
            foreach($clients as $id => $client)
            {?>
              <option value="<?= $client['CodCliente']?>"><?= $client['NomeCliente']?> </option>;
            <?php } ?>
          </select>

          <h4>Products:</h4>
          <div id="products_blocK_create">
          </div>
          <button type="button" class="btn btn-primary" onclick="addNewFilingBox()">Add a new Product</button>

          <h4>Opportunity:</h4>
          <select name="opportunity_type">
            <option value="Qualification">Qualification</option>
            <option value="Needs analysis">Needs analysis</option>
            <option value="Proposal">Proposal</option>
            <option value="Negotiations">Negotiations</option>
            <option value="Ready to close">Ready to close</option>
          </select>
          <br>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
          <input type="submit" class="btn btn-info" value="Create">
        </div>
      </form>
    </div>
  </div>
</div>


<div class="modal fade" id="moreinfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title" id="exampleModalLongTitle">Info</h1>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form action="<?= $BASE_URL?>actions/opportunity/update_oppotunity.php" method="get">
        <div class="modal-body">
        <h4>Customer id:</h4>
          <select id="customer_id" name="customer_id">
            <?php
            foreach($clients as $id => $client)
            {?>
              <option value="<?= $client['CodCliente']?>"><?= $client['NomeCliente']?> </option>;
            <?php } ?>
          </select>

          <h4>Opportunity:</h4>
          <select name="opportunity_type" id="opportunity_type">
            <option value="Qualification">Qualification</option>
            <option value="Needs analysis">Needs analysis</option>
            <option value="Proposal">Proposal</option>
            <option value="Negotiations">Negotiations</option>
            <option value="Ready to close">Ready to close</option>
          </select>
          <br>

          <input type="hidden" name="opportunity_id" id="opportunity_id">

          <h4>Activities:</h4>
          <div id="activities_block">
          </div>
          <a id="addNeEventButton"><button type="button" class="btn btn-primary">Create an Event</button></a>
          <h4>Products:</h4>
          <div id="products_block">
          </div>
          <button type="button" class="btn btn-primary" onclick="addNewFilingBoxE()">Add a new Product</button>

        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
          <input type="submit" class="btn btn-info" value="Update" name="opportunity_state">
          <input type="submit" class="btn btn-success" value="Mark as Won" name="opportunity_state">
          <input type="submit" class="btn btn-danger" value="Mark as Lost" name="opportunity_state">
          <a id="deleteButton"><button type="button" class="btn btn-danger">Delete</button></a>
        </div>
      </form>
    </div>
  </div>
</div>
<script src="<?=$BASE_URL?>vendor/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
<script src="<?=$BASE_URL?>js/opportunities.js"></script>
