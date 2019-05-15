Vue.component("widgets",{mixins:[widgetsMixin],data:()=>({dialogVisible:!1,dialogTitle:"",settingsUrl:null}),mounted(){this.initSettingsUpdatedHandler()},methods:{add(b){let c="widget-infos"===b.from.id,d={areaToId:b.to.id,index:b.newIndex,folder:null,widgetId:0,areaFromId:null,name:null,title:null};if(c)d.folder=this.widgetInfos[b.oldIndex].folder;else{let a=this.widgetAreas.find(c=>c.id===b.from.id),c=a.widgetInstances[b.oldIndex];d.folder=c.folder,d.widgetId=c.id,d.areaFromId=b.from.id,d.name=c.name,d.title=c.title}axios.post("/admin/widgets?handler=add",d,this.$root.headers).then(a=>{let d=this.widgetAreas.find(c=>c.id===b.to.id);if(c)d.widgetInstances.splice(b.newIndex,0,a.data);else{let c=this.widgetAreas.find(c=>c.id===b.from.id);c.widgetInstances.splice(b.oldIndex,1),d.widgetInstances.splice(b.newIndex,0,a.data)}}).catch(()=>{this.$root.toastError("Add widget failed.")})},sort(b){if(b.from.id!==b.to.id)return;let c=this.widgetAreas.find(c=>c.id===b.from.id),d=c.widgetInstances[b.oldIndex],e={index:b.newIndex,widgetId:d.id,areaId:b.from.id};axios.post("/admin/widgets?handler=reorder",e,this.$root.headers).then(()=>{c.widgetInstances.splice(b.oldIndex,1),c.widgetInstances.splice(b.newIndex,0,d)}).catch(()=>{this.$root.toastError("Order widget failed.")})},viewSettings(a){this.dialogTitle=a.name,this.dialogVisible=!0,this.settingsUrl=a.settingsUrl},deleteWidget(a,b){confirm(`Are you sure to delete the widget?`)&&axios.delete(`/admin/widgets?widgetId=${a.id}&areaId=${b}`,this.$root.headers).then(()=>{let c=this.widgetAreas.find(c=>c.id===b),d=c.widgetInstances.indexOf(a);c.widgetInstances.splice(d,1)}).catch(()=>{this.$root.toastError("Delete widget failed.")})},closeDialog(){this.dialogVisible=!1},initSettingsUpdatedHandler(){let a=this;window.document.addEventListener("ExtSettingsUpdated",b=>{let c=a.widgetAreas.find(c=>c.id===b.detail.areaId),d=c.widgetInstances.find(a=>a.id===b.detail.widgetId);d.title=b.detail.title,a.$root.toast(b.detail.msg),a.closeDialog()})}}});
//# sourceMappingURL=widgets.js.map