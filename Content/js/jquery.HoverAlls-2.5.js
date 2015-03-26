/*********************************************************************************************/
// $HoverAlls v2.5.1 || Crusader 12 || Exclusive to CodeCanyon
/*********************************************************************************************/	
/* EXTEND NATIVE CLASSES */
String.prototype.WS=function(){return this.toString().replace(/\s/g, '');};
String.prototype.P=Number.prototype.P=function(){return parseFloat(this);};
String.prototype.S=function(key){return this.toString().split(',')[key];};
String.prototype.isB=Boolean.prototype.isB=function(){return this.toString()=="true"?true:false;};
(function($){
var HoverAlls={
	cOBJ:null,
	d:{		
		coords:"0,0||0,0||0,0", speed:"250,250", effect:"ease,ease", opacity:"0,1",/* ANIMATION CONTAINER */
		text_coords:"0,0||0,0||0,0", text_speed:"250,250", text_effect:"ease,ease", text_opacity:"1,1",/* TEXT CONTAINER */
		overlay_speed:"400,200",/* OVERLAYS */		
		link_control:"false,_blank", /* BIG_LINK, LINK_TARGET */
		passthrough:false, /* LOOPS BACK TO THE ORIGINAL STARTING POINT */
		on_click:false, /* CHANGE TO CLICK EVENT */ on_scroll:false,
		on_start:false, start_delay:1000, /* INITIATE ON LOAD, WITH DELAY */
		target_container:false, /* CLASS NAME FOR TARGET CONTAINER */
		html_mode:false, /* INJECT YOUR OWN HTML WITH # */		
		ticker_count:1, /* NUMBER OF TIMES TO REPEAT THE TEXT IN TICKER MODE */		
		mode:false, /* TOOLTIP, CAPTION, TICKER, PANEL, LIGHTBOX, ALERT */
		text_class:false, bg_class:false, container_class:false, overlay_class:false, /* CLASSES */
		center_lightbox: "false,false",
		rotate:'0||0',
		flip:'0,0||0,0',
		scale:'1,1||1,1',
		skew:'0,0||0,0',
		text_rotate:'0||0',
		text_flip:'0,0||0,0',
		text_scale:'1,1||1,1',
		text_skew:'0,0||0,0'
	},

/* INITIALIZE */
init:function(o){
	var d=document,
		b=$('body'),
		tS=d.body.style || d.documentElement.style,
		A=navigator.userAgent.toLowerCase(),
		u=undefined,
		H=HoverAlls,
		aO='<a href="',
		dO='<div class="', 
		pO='<p class="',
		eD='></div>',
		cr='style="cursor:pointer"',			
		cW=dO+'hv_C">', 
		bW=dO+'hv_B">'+pO+'hv_text">',
		lW=dO+'hv_B" '+cr+'>'+pO+'hv_text" '+cr+'>',
		$S=$.support,
		tr='ransitionEnd',
		o=$.extend({}, H.d, o), 
		l=this.length;
		
		

	/* EXTEND $.SUPPORT FUNCTION FOR INTEGRATED FEATURE DETECTION */
	$S.Hov={
		TRNS:(tS.transition!==u||tS.WebkitTransition!==u||tS.MozTransition!==u||tS.msTransition!==u||tS.OTransition!==u)?true:false,
		PRE:(function(){
			if(/webkit/.test(A))return '-webkit-';
			if(/mozilla/.test(A)&&!/(compatible|webkit)/.test(A))return '-moz-';
			if(/msie/.test(A)&&!/opera/.test(A))return '-ms-';
			if(/opera/.test(A))return '-o-';
			return;})()
	};
	/* GET TRANSITIONEND EVENT */
	$S.Hov.TEND=(function(){
		switch ($S.Hov.PRE){
			case '-webkit-': return 'webkitT'+tr; break;
			case '-o-': return 'oT'+tr+' OT'+tr; break;
			case '-ms-': return 'MST'+tr+' transitionend'; break;
			default : return 'transitionend'; break;
	};})();




	/* ADD OVERLAY */
	if(!$('#hv_O').length)b.prepend('<div id="hv_O"></div>');	



	/* EACH EFFECT */
	for(var i=0; i<l; i++){
		/* SETUP VARS, MERGE DATA + ASSIGN DATA INFO + VALIDATE */
		var $t=$(this[i]), 
			hD=$t.data('hoveralls') != u ? $t.data('hoveralls') : false;
		$.data($t[0], $.extend({}, o, !hD ? {} : hD || {} ));
		var D=$.data($t[0]);
		

		/* I. SCRUB USER SETTINGS */
			c=D.coords.split('||'), 
			tc=D.text_coords.split('||');
		$t.data({
			sX:c[0].S(0).P(), 
			sY:c[0].S(1).P(), 
			eX:c[1].S(0).P(),
			eY:c[1].S(1).P(), 
			rX:c[2].S(0).P(), 
			rY:c[2].S(1).P(),
			tsX:tc[0].S(0).P(), 
			tsY:tc[0].S(1).P(),
			teX:tc[1].S(0).P(), 
			teY:tc[1].S(1).P(),
			trX:tc[2].S(0).P(), 
			trY:tc[2].S(1).P(),
			si:D.speed.S(0).P(), 
			so:D.speed.S(1).P(),
			ei:D.effect.S(0), 
			eo:D.effect.S(1),
			oi:D.opacity.S(0).P(), 
			oo:D.opacity.S(1).P(),
			tei:D.text_effect.S(0), 
			teo:D.text_effect.S(1),
			tsi:D.text_speed.S(0).P(), 
			tso:D.text_speed.S(1).P(),
			toi:D.text_opacity.S(0).P(), 
			too:D.text_opacity.S(1).P(),
			osi:D.overlay_speed.S(0).P(), 
			oso:D.overlay_speed.S(1).P(),
			BL:D.link_control.S(0).isB(), 
			RTI:D.rotate.split('||')[0].P(),
			RTO:D.rotate.split('||')[1].P(),
			FXI:D.flip.split('||')[0].S(0).P(),
			FYI:D.flip.split('||')[0].S(1).P(),
			FXO:D.flip.split('||')[1].S(0).P(),
			FYO:D.flip.split('||')[1].S(1).P(),			
			SCXI:D.scale.split('||')[0].S(0).P(),
			SCYI:D.scale.split('||')[0].S(1).P(),
			SCXO:D.scale.split('||')[1].S(0).P(),
			SCYO:D.scale.split('||')[1].S(1).P(),			
			SKXI:D.skew.split('||')[0].S(0).P(),
			SKYI:D.skew.split('||')[0].S(1).P(),
			SKXO:D.skew.split('||')[1].S(0).P(),
			SKYO:D.skew.split('||')[1].S(1).P(),
			TRTI:D.text_rotate.split('||')[0].P(),
			TRTO:D.text_rotate.split('||')[1].P(),
			TFXI:D.text_flip.split('||')[0].S(0).P(),
			TFYI:D.text_flip.split('||')[0].S(1).P(),
			TFXO:D.text_flip.split('||')[1].S(0).P(),
			TFYO:D.text_flip.split('||')[1].S(1).P(),
			TSCXI:D.text_scale.split('||')[0].S(0).P(),
			TSCYI:D.text_scale.split('||')[0].S(1).P(),
			TSCXO:D.text_scale.split('||')[1].S(0).P(),
			TSCYO:D.text_scale.split('||')[1].S(1).P(),
			TSKXI:D.text_skew.split('||')[0].S(0).P(),
			TSKYI:D.text_skew.split('||')[0].S(1).P(),
			TSKXO:D.text_skew.split('||')[1].S(0).P(),
			TSKYO:D.text_skew.split('||')[1].S(1).P(),			
			LT:D.link_control.S(1),
			TIC:D.ticker_count.P(), 
			M:D.mode ? D.mode.toLowerCase().WS() : false,
			Tcl:D.text_class ? D.text_class.WS() : false,
			Ccl:D.container_class ? D.container_class.WS() : false,
			Bcl:D.bg_class ? D.bg_class.WS() : false,
			Ocl:D.overlay_class ? D.overlay_class.WS() : false, 
			PT:D.passthrough.isB(),
			OC:D.on_click.isB(), 
			OS:D.on_start.isB(), 
			SCR:D.on_scroll.isB(),
			HM:D.html_mode ? D.html_mode.WS() : false, 
			TC:D.target_container ? D.target_container.WS() : false, 
			cX:D.center_lightbox.S(0).isB(), 
			cY:D.center_lightbox.S(1).isB(),
			ST:false, PN:false, LB:false, AL:false, TT:false
		});
	
	
	
		/* SAVE MODES & LIMIT INITIAL SETTINGS BASED ON USER OPTIONS */
		if(D.OC && D.M!=='panel')D.big_link=false; 
		if(D.M==='panel'){D.overlay=false; D.PN=true;};
		if(D.M==='lightbox'){D.OC=true; D.LB=true;};
		if(D.M==='alert'){D.OC=true; D.AL=true;};	
		if(D.M==='tooltip')D.TT=true;
		if(D.SCR || D.OS)D.OC=true;


		/* II. GET LINKS, TEXT AND BUILD REQUIRED HTML*/
		if(D.HM){ 
			var bWH=dO+'hv_B">',
				lWH=dO+'hv_B" '+cr+'>',				
				$L=$(D.HM).find('a.hv_link'),
				$T=$(D.HM).find('.hv_text'),
				hT=$T.length && D.HM && !D.LB && !D.AL ? $T.text() : '',
				hL=$L.length ? $L.attr('href') : false;
		
			/* CURSORS */if(D.LB || D.PN || D.AL)var cW=dO+'hv_C" '+cr+'>';
	
			/* BUILD HTML */
			if(!hL){
				if(!D.TC){ $t.wrap(cW).after(bWH);
				}else{ $(D.TC).wrap(cW).after(bWH); $t.wrap(cW); };
				var CLS=D.PN ? 'hv_PN ' : D.LB ? 'hv_LB ' : D.AL ? 'hv_AL ' : false;
				if(CLS)b.prepend(dO+CLS+D.TC+'">'+pO+'hv_text">'+hT+'</p'+eD);
			}else{ 
				if(!D.TC){ 
					if(D.BL){ 
						$t.wrap(aO+hL+'" target="'+D.LT+'">'+cW).after(lWH); 
						/* REMOVE THE ORIGINAL LINK */
						$(D.HM).find('.hv_link').remove(); 
					}else{ 
						$t.wrap(cW).after(bWH); 
					};
				}else{ 
					/* PANELS */
					if(D.PN)b.prepend(dO+'hv_PN '+D.TC+'"'+eD);

					/* LIGHTBOXES */
					if(D.LB){
						b.prepend(dO+'hv_LB '+D.TC+'"'+eD);
						/* REMOVE CURRENT LINK AND WRAP THE LIGHTBOX IN IT */
						$('.'+D.TC).wrap($L);
						if(D.HM){
							$(D.HM).find('.hv_text').css('cursor','pointer').end().find('a.hv_link').remove();
						}else{
							$('.'+D.TC).find('a.hv_link').remove();
						};
					};
				
					/* ALERTS */
					if(D.AL)b.prepend(dO+'hv_AL '+D.TC+'"'+eD); $t.wrap(cW); 
				};
			};
			

		/* HTML MODE IS OFF */ 
		}else{ 
			if($t.is('img')){ 
				var hL=$t.attr('rel') != u ? $t.attr('rel') : '',
					hT=$t.attr('alt') != u ? $t.attr('alt') : '';

			/* IF THE OBJECT IS A CONTAINER, GET THE LINK AND TEXT */
			}else{ 
				var $L=$t.find('.hv_link'),
					$T=$t.find('.hv_text'),
					hL=$L.attr('href') != u ? $L.attr('href') : false,
					hT=$T.length ? $T.text() : false;
				if($T.length)$T.remove();
				if($L.length)$L.remove();
			};
		
		
			/* BUILD HTML */
			if(!hL){
				if(!D.TC){ 
					$t.wrap(cW).after(bW);
				}else{ 
					$('.'+D.TC).wrap(cW).prepend(bW+hT); $t.wrap(cW);
				};
			
			}else{ 
				var wrap=D.BL ? aO+hL+'" target="'+D.LT+'">'+cW : cW;
			
				if(!D.TC){
					var insert=!D.BL ? aO+hL+'" target="'+D.LT+'">'+lW : lW;
					$t.wrap(wrap).after(insert);
				}else{ 
					var CLS=D.PN ? 'hv_PN ' : D.LB ? 'hv_LB ' : D.AL ? 'hv_AL ' : false;
					if(CLS)b.prepend(dO+CLS+D.TC+'">'+pO+'hv_text">'+hT+'</p'+eD);
					$t.wrap(cW);
					var	insert=D.BL ? lW : aO+hL+'" target="'+D.LT+'">'+lW;
					
					$('.'+D.TC).wrap(wrap).after(insert);
					if(D.PN || D.LB || D.AL)$t[0].style.cursor='pointer';
				};
			};
		};
	
	
		/* CACHE OBJECTS FOR FASTER SELECTION */
		var $P=$t.parent('div.hv_C'), $TP=$(D.TC).parent('div.hv_C');
		D.$C=!D.TC ? $P : $TP;
		D.$B=!D.TC ? $P.find('div.hv_B') : (D.LB||D.PN||D.AL) ? $('.'+D.TC) : $('.'+D.TC).find('div.hv_B');
		D.$T=!D.TC ? $P.find('.hv_text') : $('.'+D.TC).find('.hv_text');

		/* PRE-ANIMATION SETUP */
		$t.data({
			A:{left:D.eX+'%',top:D.eY+'%',opacity:D.oo},
			B:{left:D.rX+'%',top:D.rY+'%',opacity:D.oi},
			TA:{left:D.teX+'%',top:D.teY+'%',opacity:D.too},
			TB:{left:D.trX+'%',top:D.trY+'%',opacity:D.toi}});
			
		/* SETUP TRANSFORMATIONS */
		D.A[$S.Hov.PRE+'transform']='perspective(800px) rotate('+D.RTI+'deg) scale('+D.SCXI+','+D.SCYI+') skew('+D.SKXI+'deg,'+D.SKYI+'deg) rotateX('+D.FXI+'deg) rotateY('+D.FYI+'deg)';
		D.B[$S.Hov.PRE+'transform']='perspective(800px) rotate('+D.RTO+'deg) scale('+D.SCXO+','+D.SCYO+') skew('+D.SKXO+'deg,'+D.SKYO+'deg) rotateX('+D.FXO+'deg) rotateY('+D.FYO+'deg)';
		D.TA[$S.Hov.PRE+'transform']='perspective(800px) rotate('+D.TRTI+'deg) scale('+D.TSCXI+','+D.TSCYI+') skew('+D.TSKXI+'deg,'+D.TSKYI+'deg) rotateX('+D.TFXI+'deg) rotateY('+D.TFYI+'deg)';
		D.TB[$S.Hov.PRE+'transform']='perspective(800px) rotate('+D.TRTO+'deg) scale('+D.TSCXO+','+D.TSCYO+') skew('+D.TSKXO+'deg,'+D.TSKYO+'deg) rotateX('+D.TFXO+'deg) rotateY('+D.TFYO+'deg)';		
	
		/* USE MARGINS FOR LIGHTBOXES DUE TO CHROME ISSUE WITH POSITION:ABSOLUTE */
		if(D.LB||D.AL||D.PN){
			D.TA['margin-top']=D.TA.top; D.TA['margin-left']=D.TA.left;
			D.TB['margin-top']=D.TB.top; D.TB['margin-left']=D.TB.left;
			delete D.TA.top; delete D.TA.left;
			delete D.TB.top; delete D.TB.left;			
		}else{
			if(D.$B.length)D.$B.css({left:D.sX+'%',top:D.sY+'%'});
			if(D.$T.length)D.$T.css({left:D.tsX+'%',top:D.tsY+'%'});
		};



		/* AUTO CALL */
		if(D.OS && D.OC)setTimeout(function(){ $P.click; },D.start_delay);
	
		/* TOOLTIPS */
		if(D.TT){ D.$B.addClass('hv_TT'); D.$C[0].style.overflow='visible'; };

		/* SETUP INITIAL CSS STYLING */
		var B1={top:D.sY+'%', left:D.sX+'%', opacity:D.oi},			
			T1={top:D.tsY+'%', left:D.tsX+'%', opacity:D.toi};
				
		if(!D.TC){
			B1[$S.Hov.PRE+'transform']=D.B[$S.Hov.PRE+'transform'];
			T1[$S.Hov.PRE+'transform']=D.TB[$S.Hov.PRE+'transform'];
			
			D.$B.css(B1); 
			D.$T.css(T1).text(hT);
		}else{
			if(D.PN || D.LB)$('.'+D.TC).css(B1);
			if(D.AL)H.CTR(D,true,true);
			if(D.HM && (D.PN || D.LB || D.AL)){
				var H1={marginTop:D.tsY+'%', marginLeft:D.tsX+'%'};				
				H1[$S.Hov.PRE+'transfrom']=D.B[$S.Hov.PRE+'transform'];

				$(D.HM).find('.hv_text').css(H1);
			};
		};
		
		if(D.SCR)$t.css('cursor','default');

		/* LIGHTBOX_CENTERING */ if(D.LB && (D.cX || D.cY))H.CTR(D,D.cX,D.cY);
		/* BIG LINK CURSORS */ if(D.BL)$([D.$C[0],D.$B[0],D.$T[0]]).css('cursor','pointer');
		/* CLASSES */ if(D.Tcl)D.$T.addClass(D.Tcl); if(D.Bcl)D.$B.addClass(D.Bcl); if(D.Ccl)D.$C.addClass(D.Ccl);


		/* HTML INJECTION  */
		if(D.HM){
			var $TR=D.TC && (D.PN||D.LB||D.AL) ? $('.'+D.TC) : D.$B;
			/* REMOVE ADDITIONAL HV_TEXT IF NEEDED */
			if($(D.HM).find('.hv_text').length)$TR.find('.hv_text').remove();		
			$(D.HM).appendTo($TR);
		};


		/* TICKER MODE */
		if(D.TIC>1){
			D.$T.css('white-space','nowrap').wrapInner("<span class='hv_TSP'>");
			var RPT="<span class='hv_TSP'>"+D.$T.text()+"</span>";		
			for(var TC=1; TC<D.TIC; TC++)D.$T.append(RPT);
		};

		/* BIND EVENTS */
		if(D.OC){ 
			if(D.SCR){
				H.SCR($t,D);
			}else{
				H.CLK($t,D); 
			};
		}else{ 
			H.HVR($t,D); 
		};

	}; /* END FOR */
	
	
	/* CLOSE LIGHTBOXES, ALERTS AND PANELS */
	$('#hv_O, .close_lightbox, .close_alert, .close_panel').on('click',function(e){
		e.stopImmediatePropagation();/* ONLY FIRE EVENT ONCE - DUE TO MULTIPLE PLUGIN CALLS */		
		var $t=$(this),		
			o=$t.hasClass('close_panel') ? $t.parents('div.hv_PN:first').data().initiator : HoverAlls.cOBJ;
		HoverAlls.CLSE(o,$.data(o[0]));
		return false;
	});
},









/* HOVER EVENTS */
HVR:function(o,D){
	o.parent('div.hv_C').on('mouseenter',function(e){ HoverAlls.OPN(o,D);
	}).on('mouseleave',function(){ HoverAlls.CLSE(o,D); });		
},

/* CLICK EVENTS */
CLK:function(o,D){
	o.parent('div.hv_C').on('click',function(){
		if(D.ST){ HoverAlls.CLSE(o,D); /* CLOSE */ 
		}else{ HoverAlls.OPN(o,D); };  /* OPEN */ 	
		return false; 
	});
},

/* SCROLL EVENTS */
SCR:function(o,D){
	$(document).on('scroll',function(){ 	
    	var dT=$(window).scrollTop(),
	  		dB=dT+$(window).height(),
   			eT=o.offset().top,
    		eB=eT+o.height(),
			inView=(eB <= dB) && (eT >= dT);
		/* IF IN VIEW DISPLAY ANIMATION */		
		if(inView){
			if(!D.ST)HoverAlls.OPN(o,D);
		}else{
			if(D.ST)HoverAlls.CLSE(o,D); 
		};
	}).scroll();
},










/* OPEN METHOD */
OPN:function(o,D){
	D.ST=true;
	HoverAlls.cOBJ=o;
	
	/* HANDLE FAST HOVERING FOR TOOLTIPS && TICKERS */
	if(!D.OC){
		if((D.TT && D.$B.css('opacity').P()>0) || $('#hv_O').css('opacity').P()>0) return;
		/* PREVENT MOUSEENTER ON TOOLTIP CONTAINERS */
		if(D.TT)D.$B.css('visibility','visible');
	};

	/* ANIMATE CONTAINERS */
	if(!D.TC){
		D.$B.An(D.A,D.si,D.ei,function(){
			if(!D.OC && D.TT) D.$B.css('visibility','visible'); 
		});
		D.$T.An(D.TA,D.tsi,D.tei,null);		

			
	/* ANIMATE TARGET_CONTAINERS */
	}else{ 
		/* PANELS */
		if(D.PN){
			var $CO=D.HM ? $(D.HM).find('.hv_text') : D.$T;
			$('.'+D.TC).css({left:D.sX+'%',top:D.sY+'%',opacity:D.oo}).An(D.A,D.si,D.ei,null)
			/* MUST ASSIGN ORIGINAL OBJECT TO PANEL TARGET CONTAINER FOR .CLOSING */
			.data({initiator:o});
			$CO.css({marginLeft:D.tsX,marginTop:D.tsY}).An(D.TA,D.tsi,D.tei,null);

		}else{ 
			if(D.OC){
				
				/* LIGHTBOXES & ALERTS */
				if(D.LB||D.AL){
					/* SET OVERLAY CLASS */
					var $O=$('#hv_O');
					if(D.Ocl)$O[0].className=D.Ocl;
					$O.css({visibility:'visible',width:'100%',height:'100%'})
					.An({'opacity':.9},D.osi,'ease',null);

					$('.'+D.TC).css('visibility','visible');
						
					/* CENTER ALERTBOXES AND LIGHTBOXES */
					var X=D.AL ? true : D.cX, Y=D.AL ? true : D.cY;
					HoverAlls.CTR(D,X,Y); 
						
					/* ANIMATE LIGHTBOX IN */
					$('.'+D.TC).An(D.A,D.si,D.ei,function(){
						$('.'+D.TC)[0].style.visibility='visible';
					}).find('.hv_text').An(D.TA,D.tsi,D.tei,null)
				
				}else{
					D.$B.css({left:D.sX,top:D.sY})
					.find('.hv_text').css({left:D.tsX,top:D.tsY}).An(D.A,D.si,D.ei,null)
					.find('.hv_text').An(D.TA,D.tsi,D.tei,null);
				};		
			}else{
				D.$B.An(D.A,D.si,D.ei,null);
				D.$T.An(D.TA,D.tsi,D.tei,null);
			};
		};
	};	
},





/* CLOSE METHOD */
CLSE:function(o,D){
	D.ST=false;

	/* PANELS */
	if(D.PN){ 
		var $C=D.HM ? $(D.HM).find('.hv_text') : D.$T;
		$C.An(D.TB,D.tso,D.teo,function(){ if(D.PT)$C.css({top:D.tsY+'%',left:D.tsX+'%'}) }); 						
		$('.'+D.TC).An(D.B,D.so,D.eo,null);						

	}else{ 
		/* CLOSE OVERLAY */	
		if(D.OC && (D.LB||D.AL)) $('#hv_O').An({opacity:0}, D.oso, 'ease', function(){ $(this).css({width:0,height:0}); });		
		
		var $CL=D.OC ? $('.'+D.TC).find('.hv_text') : D.$T;
		
		/* TEXT OUT */
		$CL.An(D.TB,D.tso,D.teo,function(){ 
			if(D.PT){
				D.$B.css({top:D.sY+'%',left:D.sX+'%'});
				$CL.css({top:D.tsY+'%',left:D.tsX+'%'});
			};
		}); 
			
		/* CONTAINER OUT */
		D.$B.An(D.B,D.so,D.eo,function(){ 
			if(!D.OC){
				/* PREVENT TRIGGERING MOUSEENTER ON TOOLTIP CONTAINERS */				
				if(D.TT)D.$B.css('visibility','hidden');
			}else{
				if(!D.SCR)D.$B.css('visibility','hidden');
			};
		});
	};	
},
	





/* CENTER ALERT BOXES */
CTR:function(D,X,Y){
	var W=$(window), $TC=$('.'+D.TC),
		WW=W[0].innerWidth ? W[0].innerWidth : $(W).width(),
		WH=W[0].innerHeight ? W[0].innerHeight : $(W).height(),		
		l=(((WW-$TC.outerWidth(true))/2)/WW)*100+'%',
		t=(((WH-$TC.outerHeight(true))/2)/WH)*100+'%';
	if(X){ D.A.left=l; D.B.left=l; $TC[0].style.left=l; };
	if(Y){ D.A.top=t; D.B.top=t; $TC[0].style.top=t; };
}






};

/* EXTEND JQUERY FUNCTIONALITY */
$.fn.extend({
An:function(Ani,d,e,cF){
	/* ANI=ANIMATION ARGUMENTS, D=DURATION, CF=COMPLETE FUNCTION */
	var o=$(this), S='', $S=$.support.Hov;

	/* CSS3 */
	if($S.TRNS){
		for(var k in Ani){if(Ani.hasOwnProperty(k))S+=''+k+' '+d/1000+'s '+e+' 0s,';};
		Ani.transition=S.replace(/,+$/,"");
		o.css(Ani).one($S.TEND,function(e){
			/* REMOVE TransEnd TO PREVENT MULTIPLE FIRING (EVEN WITH .ONE) */
			o.off($S.TEND);
			if(typeof cF==='function')cF.apply(this,arguments);
			e.stopPropagation(); 
		});

	/* JQUERY */
	}else{
		o.stop(true,false).animate(Ani,{duration:d,queue:false,complete:function(){
			if(typeof cF==='function')cF.apply(this,arguments);
		}});
	};
	return o;	
}});


$.fn.HoverAlls=function(method,o){
	if(HoverAlls[method]){ return HoverAlls[method].apply(this,Array.prototype.slice.call(arguments,1));
	}else if(typeof method==='object'||!method){ return HoverAlls.init.apply(this,arguments);
	}else{ $.error('Method '+method+' does not exist'); }
}})(jQuery);