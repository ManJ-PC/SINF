	<div class="page-header">

		<div class="pull-right form-inline">
			<div class="btn-group">
				<button class="btn btn-success" onclick=" relocate_home()">Create</button>
			</div>
			<div class="btn-group">
				<button class="btn btn-primary" data-calendar-nav="prev">Prev</button>
				<button class="btn btn-default" data-calendar-nav="today">Today</button>
				<button class="btn btn-primary" data-calendar-nav="next">Next</button>
			</div>
			<div class="btn-group">
				<button class="btn btn-warning" data-calendar-view="year">Year</button>
				<button class="btn btn-warning active" data-calendar-view="month">Month</button>
				<button class="btn btn-warning" data-calendar-view="week">Week</button>
				<button class="btn btn-warning" data-calendar-view="day">Day</button>
			</div>
		</div>

		<h3></h3>
	</div>

	<div class="row">
			<div id="calendar"></div>
	</div>

	<div class="clearfix"></div>
	<br><br>

	<div class="modal fade" id="events-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h3 class="modal-title">Event</h3>
				</div>
				<div class="modal-body" style="height: 400px">
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
				</div>
			</div>
		</div>
	</div>
	
	<script>
	function relocate_home()
	{
		 location.href = "<?=$BASE_URL?>pages/create_event.php";
	} 
	</script>

	<script type="text/javascript" src="<?=$BASE_URL?>bower_components/underscore/underscore-min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
	
