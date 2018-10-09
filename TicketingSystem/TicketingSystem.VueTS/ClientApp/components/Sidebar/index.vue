<template>
	<nav class="col-md-2 sidebar navbar navbar-expand-md" style="align-items:baseline">
		<div class="collapse navbar-collapse" id="navbarSupportedContent">
			<div class="sidebar-sticky">
				<div role="tablist">
					<div v-for="item in sidebarItems">
						<b-card no-body v-if="item.children">
							<b-card-header header-tag="header" role="tab">
								<a class="nav-link"
								   @click="onClick(item)"
								   v-bind:class="item.isOpen ? 'collapsed sidebar-expanded' : null"
								   aria-controls="item.id"
								   :aria-expanded="item.isOpen ? 'true' : 'false'">
									<font-awesome-icon :icon="item.icon">
									</font-awesome-icon>
									{{item.text}}
									<font-awesome-icon v-if="item.isOpen" icon="angle-down"
													   class="arrow">
									</font-awesome-icon>
									<font-awesome-icon v-else icon="angle-left"
													   class="arrow">
									</font-awesome-icon>
								</a>
							</b-card-header>
							<b-collapse id="item.id" v-model="item.isOpen">
								<b-card-body class="b-card-children">
									<ul class="nav flex-column">
										<li v-for="child in item.children" class="nav-item">
											<b-card no-body v-if="child.children">
												<b-card-header header-tag="header" role="tab">
													<a class="nav-link"
													   @click="onClick(child)"
													   v-bind:class="child.isOpen ? 'collapsed sidebar-expanded' : null"
													   aria-controls="child.id"
													   :aria-expanded="child.isOpen ? 'true' : 'false'">
														<font-awesome-icon :icon="child.icon">
														</font-awesome-icon>
														{{child.text}}
														<font-awesome-icon v-if="child.isOpen" icon="angle-down"
																		   class="arrow">
														</font-awesome-icon>
														<font-awesome-icon v-else icon="angle-left"
																		   class="arrow">
														</font-awesome-icon>
													</a>
												</b-card-header>
												<b-collapse id="child.id" v-model="child.isOpen">
													<b-card-body class="b-card-children">
														<ul class="nav flex-column">
															<li class="nav-item" v-for="innerChild in child.children">
																<b-link class="nav-link active"
																		:to="innerChild.url"
																		@click="onClickInner(innerChild)"
																		v-bind:class="innerChild.isSelected ? 'sidebar-selected' : null">
																	<font-awesome-icon :icon="innerChild.icon">
																	</font-awesome-icon>
																	{{innerChild.text}}
																</b-link>
															</li>
														</ul>
													</b-card-body>
												</b-collapse>
											</b-card>
											<b-link v-else class="nav-link active"
													:to="child.url"
													@click="onClick(child)"
													v-bind:class="child.isSelected ? 'sidebar-selected' : null">
												<font-awesome-icon :icon="child.icon">
												</font-awesome-icon>
												{{child.text}}
											</b-link>
										</li>
									</ul>
								</b-card-body>
							</b-collapse>
						</b-card>
						<b-card no-body v-else>
							<b-link class="nav-link"
									:to="item.url"
									@click="onClick(item)"
									v-bind:class="item.isSelected ? 'sidebar-selected' : null">
								<font-awesome-icon :icon="item.icon">
								</font-awesome-icon>
								{{item.text}}
							</b-link>
						</b-card>
					</div>
				</div>
			</div>
		</div>
	</nav>
</template>

<script src="./index.ts"></script>

<style scoped>
	.sidebar-sticky {
		width: 100%;
	}

	.b-card-children {
		margin: 5px 0;
	}
</style>
