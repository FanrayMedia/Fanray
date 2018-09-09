Vue.component("blog-media",{template:"#blog-media-template",mixins:[blogMediaMixin],props:["mode"],data:()=>({editDialogVisible:!1,progressDialog:!1,pageNumber:1,selectedImages:[],selectedImageIdx:0,errMsg:"",isEditor:!1}),mounted(){this.initWindowDnd(),"editor"===this.mode&&(this.isEditor=!0,this.initImages())},computed:{showMoreVisible:function(){return this.count>this.images.length},leftArrowVisible:function(){return 1<this.selectedImages.length&&0<this.selectedImageIdx},rightArrowVisible:function(){return 1<this.selectedImages.length&&this.selectedImageIdx<this.selectedImages.length-1}},methods:{initWindowDnd(){window.addEventListener("dragenter",function(){document.querySelector("#dropzone").style.visibility="",document.querySelector("#dropzone").style.opacity=1}),window.addEventListener("dragleave",function(a){a.preventDefault(),document.querySelector("#dropzone").style.visibility="hidden",document.querySelector("#dropzone").style.opacity=0}),window.addEventListener("dragover",function(a){a.preventDefault(),document.querySelector("#dropzone").style.visibility="",document.querySelector("#dropzone").style.opacity=1});let a=this;window.addEventListener("drop",function(b){b.preventDefault(),document.querySelector("#dropzone").style.visibility="hidden",document.querySelector("#dropzone").style.opacity=0,a.dragFilesUpload(b.dataTransfer.files)})},dragFilesUpload(a){this.progressDialog=!0;const b=new FormData;a.length&&(Array.from(Array(a.length).keys()).map(c=>{b.append("images",a[c])}),this.sendImages(b))},chooseFilesUpload(){const a=document.createElement("input");a.setAttribute("type","file"),a.setAttribute("accept","image/*"),a.setAttribute("multiple",null),a.click(),a.onchange=()=>{this.progressDialog=!0;const b=new FormData;for(let c=0;c<a.files.length;c++)b.append("images",a.files[c]);this.sendImages(b)}},sendImages(a){axios.post("/admin/media?handler=image",a,this.$root.headers).then(a=>{if(0<a.data.images.length){for(var b=0;b<a.data.images.length;b++)this.images.unshift(a.data.images[b]);this.count+=a.data.images.length,this.$root.toast("Image uploaded.")}a.data.errorMessage&&(this.errMsg=a.data.errorMessage),this.progressDialog=!1}).catch(()=>{this.progressDialog=!1,this.$root.toast("Image upload failed.","red")})},initImages(){axios.get("/admin/media?handler=images").then(a=>{this.images=a.data.medias,this.count=a.data.count}).catch(()=>void 0)},selectImage(a){let b=this.selectedImages.findIndex(b=>b.id===a.id);-1===b?(a.selected=!0,this.selectedImages.push(a)):(a.selected=!1,this.selectedImages.splice(b,1))},leftArrow(){this.selectedImageIdx--},rightArrow(){this.selectedImageIdx++},showMore(){this.pageNumber++,this.images.length<this.pageSize&&this.pageNumber--;let a=`/admin/media?handler=more&pageNumber=${this.pageNumber}`;axios.get(a).then(a=>{for(var b,c=0;c<a.data.length;c++)b=this.images.some(function(b){return b.id===a.data[c].id}),b||this.images.push(a.data[c])}).catch(()=>void 0)},editImages(){this.editDialogVisible=!0},deleteImages(){if(confirm("Are you sure you want to delete the image(s)? They will no longer appear anywhere on your website. This cannot be undone!")){const b=this.selectedImages.length;let c=[];for(var a=0;a<b;a++)c.push(this.selectedImages[a].id);axios.post("/admin/media?handler=delete",c,this.$root.headers).then(()=>{for(var a=0;a<b;a++){let b=this.images.findIndex(b=>b.id===this.selectedImages[a].id);this.images.splice(b,1)}this.selectedImages=[],this.count-=b,this.$root.toast("Image deleted.")}).catch(()=>{this.$root.toastError("Image delete failed.")})}},updateImage(){axios.post("/admin/media?handler=update",this.selectedImages[this.selectedImageIdx],this.$root.headers).then(()=>{this.$root.toast("Image updated.")}).catch(()=>{this.$root.toastError("Image update failed.")})},closeEditDialog(){this.selectedImageIdx=0,this.editDialogVisible=!1}}});
//# sourceMappingURL=blog-media.js.map