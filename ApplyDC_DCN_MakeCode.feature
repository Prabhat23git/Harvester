Feature: UI_ApplyDC_DCN_MakeCode

@ApplyDC_DCN @Positive
Scenario:TC2.1 | FeedConfig | Verify that Entire Service provider list is displayed

	Given check table
	Then verifytable


	@ApplyDcDcn@positive
Scenario Outline:TC3.1 | Apply DC_DCN | Verify that user navigated to page DC_DCN successfully

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	Then Verify that page DC_DCN Loaded successfully

Examples: 
| provider     | username                | password         | 
| Volvo        | aemp@aertssen.be        | abc1234          | 
| Trimble      | kiewit_aemp             | aemp_2014        |	
| Liebherr     | kiewit\kiewitwebservice | >I(U+TI9ZM       | 
| OEM Aemp 1.2 | Admin203                | abc1234          | 
| XacTrac      | 387\kiewit.mis.1        | 3UP7YJ           | 
| LHP          | kiewitaemp              | 9xe16zmcj62xa5cq | 

	@ApplyDcDcn@positive
Scenario Outline:TC3.2 | Search_SerialNo_DCDCN | SearchWith SerialNo_DC_DCNPage 

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	When User search with <Serial_no>
	Then Verify that searched <Serial_no> is displayed

Examples: 

| provider     | username                | password         | Serial_no |
| Volvo        | aemp@aertssen.be        | abc1234          | A35       |
| Trimble      | kiewit_aemp             | aemp_2014        |           |
| Liebherr     | kiewit\kiewitwebservice | >I(U+TI9ZM       |           |
| OEM Aemp 1.2 | Admin203                | abc1234          |           |
| XacTrac      | 387\kiewit.mis.1        | 3UP7YJ           |           |
| LHP          | kiewitaemp              | 9xe16zmcj62xa5cq |           |

	@ApplyDcDcn@positive
Scenario Outline:TC3.3 | Sort_Column_DCDCN|Verify SortColumn_DCDCNPage 

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	Then Verify that <column> are sorted in <OrderBy> order
	
Examples: 
	| provider | username         | password | column           | OrderBy |
	| Volvo    | aemp@aertssen.be | abc1234  | Serial Number    | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Serial Number    | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Manufacture Desc | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Manufacture Desc | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Dealer           | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Dealer           | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Business Unit    | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Business Unit    | Desc    |

	@ApplyDcDcn@positive
Scenario Outline:TC3.4 | Filter_DCDCN|Verify FilterColumn_DCDCNPage 

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	When User Filter <column> with <FilterText>
	Then Verify that <column> filter results are displayed for <FilterText>
	
Examples: 
	| provider | username         | password | column           | FilterText |
	| Volvo    | aemp@aertssen.be | abc1234  | Manufacture Desc | PAL        |
	| Volvo    | aemp@aertssen.be | abc1234  | Dealer           | TD00       |
	| Volvo    | aemp@aertssen.be | abc1234  | Business Unit    | Brian      |

	@ApplyDcDcn@positive
	Scenario Outline:TC3.5 | Download_DCDCNPage|Verify Download CSV

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	When User Export the csv file
	Then Verify the Exported csv file
	
Examples: 
	| provider | username         | password | 
	| Volvo    | aemp@aertssen.be | abc1234  | 

	@ApplyDcDcn@positive
	Scenario Outline:TC3.6 | ToolTip Message_ApplyDcDCNPage|Verify Tooltip message is displayed_ApplyDcDCNPage

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	When Mouse Hover to ToolTip error
	Then Verify ToolTip Message
	
Examples: 
	| provider | username         | password | OrgID |
	| Volvo    | aemp@aertssen.be | abc1234  | 2772  |

	@ApplyDcDcn@positive
	Scenario Outline:TC3.7 | Error Count_ApplyDcDCNPage |Verify Error count is displayed for unregistered asset_ApplyDcDCNPage

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	Then Verify Error Count for <username> <orgid>
	
Examples: 
	| provider | username         | password | orgid |
	| Volvo    | aemp@aertssen.be | abc1234  | 2772  |

	@ApplyDcDcn@positive
	Scenario Outline:TC3.8 | RegisterBusiness_Dealer_MakeCode |Verify That asset is registered successfully

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	When User Apply <Dealer> and <BusinessUnit>
	Then Click on Save_next button on Page Apply Dc_Dcn
	And Apply <MakeCode> <Year> <vin> to same Asset
	And Verify Asset is registered Successfully for <username> <orgid>
	
Examples: 
	| provider | username         | password | orgid | Dealer | BusinessUnit    | MakeCode | Year | vin |
	| Volvo    | aemp@aertssen.be | abc1234  | 2772  | TD00   | BRIAN CARPENTER | PAL      | 2017 | Vin |


#--------------------------------------------------------Page_ApplyMakeCode-------------------------------------------------------------------------------

	@ApplyMakeCode@positive
Scenario Outline:TC4.1 | ApplyMakeCode | Verify that user navigated to page ApplyMakeCode successfully

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	Then Verify that page ApplyMakeCode Loaded successfully

Examples: 
| provider     | username                | password         | 
| Volvo        | aemp@aertssen.be        | abc1234          | 
| Trimble      | kiewit_aemp             | aemp_2014        | 
| Liebherr     | kiewit\kiewitwebservice | >I(U+TI9ZM       | 
| OEM Aemp 1.2 | Admin203                | abc1234          | 
| XacTrac      | 387\kiewit.mis.1        | 3UP7YJ           | 
| LHP          | kiewitaemp              | 9xe16zmcj62xa5cq | 

@ApplyMakeCode@positive
Scenario Outline:TC4.2 | Search_SerialNo_ApplyMakeCode | SearchWith SerialNo_ApplyMakeCode Page

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	When User search with <Serial_no>
	Then Verify that searched <Serial_no> is displayed

Examples: 

| provider     | username                | password         | Serial_no |
| Volvo        | aemp@aertssen.be        | abc1234          | A35       |
| Trimble      | kiewit_aemp             | aemp_2014        |           |
| Liebherr     | kiewit\kiewitwebservice | >I(U+TI9ZM       |           |
| OEM Aemp 1.2 | Admin203                | abc1234          |           |
| XacTrac      | 387\kiewit.mis.1        | 3UP7YJ           |           |
| LHP          | kiewitaemp              | 9xe16zmcj62xa5cq |           |

@ApplyDcDcn@positive
Scenario Outline:TC4.3 | Sort_Column_DCDCN|Verify SortColumn_ApplyMakeCodePage 

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	Then Verify that <column> are sorted in <OrderBy> order
	
Examples: 
	| provider | username         | password | column           | OrderBy |
	| Volvo    | aemp@aertssen.be | abc1234  | Serial Number    | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Serial Number    | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Manufacture Desc | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Manufacture Desc | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Make Code        | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Make Code        | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset ID         | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset ID         | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset Model      | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset Model      | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Model Year       | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Model Year       | Desc    |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset VIN        | Asc     |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset VIN        | Desc    |



	@ApplyDcDcn@positive
Scenario Outline:TC4.4 | Filter_ApplyMakeCodePage|Verify FilterColumn_ApplyMakeCodePage 

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	When User Filter <column> with <FilterText>
	Then Verify that <column> filter results are displayed for <FilterText>
	
Examples: 
	| provider | username         | password | column           | FilterText |
	| Volvo    | aemp@aertssen.be | abc1234  | Manufacture Desc | Volvo      |
	| Volvo    | aemp@aertssen.be | abc1234  | Make Code        | PAL        |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset ID         | A25F       |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset Model      | A25F       |
	| Volvo    | aemp@aertssen.be | abc1234  | Model Year       | 2017       |
	| Volvo    | aemp@aertssen.be | abc1234  | Asset VIN        | Vin        |

	@ApplyDcDcn@positive
	Scenario Outline:TC4.5 | Download_ApplyMakeCodePage|Verify Download CSV

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	When User Export the csv file
	Then Verify the Exported csv file
	
Examples: 
	| provider | username         | password | 
	| Volvo    | aemp@aertssen.be | abc1234  | 


	@ApplyDcDcn@positive
	Scenario Outline:TC4.6 | ToolTip Message_ApplyMakeCodePage|Verify Tooltip message is displayed_ApplyMakeCode Page

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	When Mouse Hover to ToolTip error
	Then Verify ToolTip Message_ApplyMakeCodePage
	
Examples: 
	| provider | username         | password | OrgID |
	| Volvo    | aemp@aertssen.be | abc1234  | 2772  |

	@ApplyDcDcn@positive
	Scenario Outline:TC4.7 | Error Count_ApplyMakeCodePage |Verify Error count is displayed for unregistered asset_ApplyMakeCodePage

	Given Navigate to Page_ApllyDCDCN for <provider> having <username> <password>
	And  Navigate to Page_ApplyMakeCode
	Then Verify Error Count for <username> <orgid>
	
Examples: 
	| provider | username         | password | orgid |
	| Volvo    | aemp@aertssen.be | abc1234  | 2772  |


	@ApplyDcDcn@positive
	Scenario Outline:TC10 |Get Json For Calamp

	Given  Get the Jsonfor Clamp where FeedId <FeedId>
	
Examples: 
	| FeedId                               |
	| B4515D35-1CD7-4A02-AC1A-7E5D257A8F4B|

	
