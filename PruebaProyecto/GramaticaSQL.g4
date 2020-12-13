grammar GramaticaSQL;

/*
 * Parser Rules
 */

consulta: SELECT atributo ESP from tabla postInfo fin ;

atributo: ESP aste #atrAst
		| ESP NOM'.'NOM atributo ?  #atrTab;
		 
postInfo: condicion | inner ;

inner: (ESP 'INNER JOIN' ESP NOM ESP 'ON' ESP NOM'.'NOM ESP '=' ESP NOM'.'NOM)? #conInnerJoin;

from: 'FROM';

aste: '*';

tabla: ESP NOM tabla? #nomTabla
	  /*|ESP NOM tabla ?  #recurTab*/;

condicion: (ESP WHERE ESP NOM'.'NOM ESP operador ESP comparador masCondi )?  #conCondicion;

operador: '=' | '<>' | '>' | '>=' | '<' | '<=';

comparador: NOM | NUM;

masCondi: (ESP 'AND'ESP NOM'.'NOM ESP operador ESP comparador masCondi) ? #condicionUnica
		  ;

fin:EOF   #finConsulta;


WHERE: 'WHERE';


SELECT: 'SELECT';

NOM: [A-Z]+;


NUM: [0-9]+;

ESP: ' '; 


//: 'Int' | 'Double' | 'Float';


 /*
 consulta: SELECT ESP atributos EOF;// ESP FROM ESP NOMTAB EOF;

 //atributos: ASTER | NOMATR;
 atributos:atributos atributo
		  | atributo;

 atributo: ASTER 
		 | NOMATR ;*/

 

/*
 * Lexer Rules
 */

 /*
SELECT: 'SELECT';

NOMATR: [A-Z]+;

ESP: ' '; 

ASTER: '*';

FROM: 'FROM';

NOMTAB: [A-Z]+;

FINL:['\r\n']+;*/

/*compileUnit
	:	EOF
	;*/


/*
WS
	:	' ' -> channel(HIDDEN)
	;
*/